using UnityEngine;

public class Minion : MonoBehaviour
{
    [Header("References")]
    public int health; // Health of the minion
    public float detectionRange = 20f; // Range to detect nearest turret

    private GameObject toFollow; // Object that the minion will follow
    private GameObject[] turrets; // List of all turrets in the scene

    private void Start()
    {
        // Find all objects tagged as "Turret" in the scene
        turrets = GameObject.FindGameObjectsWithTag("Turret");

        // Find the nearest turret at the start
        toFollow = FindNearestTurret();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the player as the target to follow
            toFollow = other.gameObject;
            Debug.Log("Player entered detection range. Following player.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // When the player leaves the trigger zone, switch back to the nearest turret
            toFollow = FindNearestTurret();
            Debug.Log("Player exited detection range. Returning to nearest turret.");
        }
    }

    // Method to find the nearest turret
    private GameObject FindNearestTurret()
    {
        GameObject nearestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject turret in turrets)
        {
            // Calculate the distance between the minion and the turret
            float distance = Vector3.Distance(transform.position, turret.transform.position);

            // Check if this turret is closer than the current nearest turret
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

    private void Update()
    {
        // Follow the current target (if one exists)
        if (toFollow != null)
        {
            // Move towards the target (optional: add smoothing)
            transform.position = Vector3.MoveTowards(transform.position, toFollow.transform.position, Time.deltaTime * 30f);
        }
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
