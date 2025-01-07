using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [Header("References")]
    public int health; // Health of the minion
    public float detectionRange = 20f; // Range to detect nearest turret
    private bool canMove = true;

    private GameObject toFollow; // Object that the minion will follow

    private Animator _animator;
    private bool isInHittingRange = false;
    public int damagePerSecond = 10; // Damage dealt per second
    private float damageTimer = 0f;

    private void Start()
    {

        // Find the nearest turret at the start
        toFollow = FindNearestTurret();

        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the player as the target to follow
            canMove = true;
            toFollow = other.gameObject;
            Debug.Log("Player entered detection range. Following player.");
            _animator.SetBool("isHitting", false);
        }

        if (other.CompareTag("TurretHittingRange"))
        {
            canMove = false;
            isInHittingRange = true;
            _animator.SetBool("isHitting", true);
        }

        //if (other.CompareTag("PlayerHittingRange"))
        //{
        //    canMove = false;
        //    isInHittingRange = true;
        //    _animator.SetBool("isHitting", true);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // When the player leaves the trigger zone, switch back to the nearest turret
            toFollow = FindNearestTurret();
            Debug.Log("Player exited detection range. Returning to nearest turret.");
        }

        if (other.CompareTag("TurretHittingRange") /*|| other.CompareTag("PlayerHittingRange")*/)
        {
            canMove = true;
            isInHittingRange = false;
            _animator.SetBool("isHitting", false);
            Debug.Log("Minion exited turret hitting range");
        }
    }

    private void Update()
    {
        // Make the minion look at the target
        LookAtTarget();

        // Follow the current target if movement is allowed
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, toFollow.transform.position, Time.deltaTime * 30f);
        }

        // Deal damage if in hitting range
        if (isInHittingRange)
        {
            DealDamageOverTime();
        }
    }


    private void OnEnable()
    {
        Health.TurretDestroyed += OnTargetDestroyed;
    }

    private void OnDisable()
    {
        Health.TurretDestroyed -= OnTargetDestroyed;
    }

    private void OnTargetDestroyed(GameObject destroyedObject)
    {
        if (toFollow == destroyedObject)
        {
            Debug.Log("Target destroyed. Finding a new target...");
            StartCoroutine(DelayFindNearestTurret());
        }
    }

    private IEnumerator DelayFindNearestTurret()
    {
        yield return null; // Wait for one frame to ensure UnregisterTurret is processed

        toFollow = FindNearestTurret();

        if (toFollow == null)
        {
            // Reset state if no targets remain
            canMove = false;
            isInHittingRange = false;
            _animator.SetBool("isHitting", false);
            Debug.Log("No targets to follow. Minion is idle.");
        }
        else
        {
            isInHittingRange = false;
            _animator.SetBool("isHitting", false);
            canMove = true;
        }
    }

    private void LookAtTarget()
    {
        if (toFollow != null)
        {
            // Get the direction to the target
            Vector3 direction = (toFollow.transform.position - transform.position).normalized;

            // Create a rotation that looks at the target, preserving the minion's vertical axis
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Apply an offset to the rotation (adjust based on your model's forward axis)
            Quaternion adjustedRotation = lookRotation * Quaternion.Euler(-90, 0, 0); // Example offset

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, adjustedRotation, Time.deltaTime * 5f);
        }
    }


    private void DealDamageOverTime()
    {
        if (toFollow == null)
        {
            Debug.Log("Target destroyed during hitting. Resetting state.");
            toFollow = FindNearestTurret();
            isInHittingRange = false;
            _animator.SetBool("isHitting", false); // Stop hitting animation
            canMove = true;
            return;
        }

        damageTimer += Time.deltaTime;

        // Apply damage once per second
        if (damageTimer >= 1f)
        {
            damageTimer = 0f; // Reset the timer

            // Check if the object being followed has a health system
            Health targetHealth = toFollow.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damagePerSecond);
                Debug.Log($"Minion dealt {damagePerSecond} damage to {toFollow.name}");
            }
            else
            {
                Debug.LogWarning($"{toFollow.name} does not have a Health component!");
            }
        }
    }


    // Method to find the nearest turret
    private GameObject FindNearestTurret()
    {
        List<GameObject> turrets = TurretManager.Instance.GetActiveTurrets();

        Debug.Log("Active Turrets Count: " + turrets.Count);

        foreach (GameObject turret in turrets)
        {
            //Debug.Log(turret.name);
            if (turret == null)
            {
                Debug.LogWarning("Found a null turret in the list!");
            }
        }

        GameObject nearestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject turret in turrets)
        {
            if (turret == null) continue; // Skip null turrets

            float distance = Vector3.Distance(transform.position, turret.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTurret = turret;
            }
        }

        if (nearestTurret != null)
        {
            Debug.Log($"Nearest turret found: {nearestTurret.name}");
        }
        else
        {
            Debug.Log("No turrets found!");
        }

        return nearestTurret;
    }


    // Method to handle taking damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Minion took {amount} damage. Remaining health: {health}");

        // Destroy the minion if health drops to zero or below
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Minion destroyed!");
        }
    }
}
