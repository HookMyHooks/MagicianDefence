using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireringCollision : MonoBehaviour
{
    [Header("References")]
    public int damage; // Damage dealt by the fireball

    private float turretDamageTimer = 0f;
    private float minionDamageTimer = 0f;
    private float playerDamageTimer = 0f;

    // Time interval in seconds
    private float damageInterval = 2f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Turret"))
        {
            Turret turret = GameObject.FindAnyObjectByType<Turret>();
            // Apply damage to the turret
            turret.TakeDamage(damage);
        }

        if(collision.CompareTag("Minion"))
        {
            Minion enemyMinion = GameObject.FindAnyObjectByType<Minion>();

            enemyMinion.TakeDamage(damage);
        }

        if (collision.CompareTag("Player"))
        {
            Wizard player = GameObject.FindAnyObjectByType<Wizard>();
            // Apply damage to the turret
            player.TakeDamage(damage);

            Debug.Log("Wizzard took damage onEnter");
        }


        //// Check if the object hit has the "Turret" component
        //Turret turret = collision.gameObject.GetComponent<Turret>();

        //if (collision.gameObject.transform.parent?.tag == "Turret")
        //{
        //    SceneManager.LoadScene("VictoryScreen");
        //}

        //string obj = collision.gameObject.ToString();

        //Debug.Log($"Fireball collision detected with:{obj}");

        //if (turret != null)
        //{
        //    // Apply damage to the turret
        //    turret.TakeDamage(damage);

        //    // Optionally destroy the fireball on impact
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Turret"))
        {
            turretDamageTimer += Time.deltaTime;
            if (turretDamageTimer >= damageInterval)
            {
                Turret turret = collision.GetComponent<Turret>();
                if (turret != null)
                {
                    turret.TakeDamage(damage);
                }
                turretDamageTimer = 0f; // Reset the timer
            }
        }

        if (collision.CompareTag("Minion"))
        {
            minionDamageTimer += Time.deltaTime;
            if (minionDamageTimer >= damageInterval)
            {
                Minion enemyMinion = collision.GetComponent<Minion>();
                if (enemyMinion != null)
                {
                    enemyMinion.TakeDamage(damage);
                }
                minionDamageTimer = 0f; // Reset the timer
            }
        }

        if (collision.CompareTag("Player"))
        {
            playerDamageTimer += Time.deltaTime;
            if (playerDamageTimer >= damageInterval)
            {
                Wizard player = collision.GetComponent<Wizard>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    Debug.Log("Wizard took damage on Stay");
                }
                playerDamageTimer = 0f; // Reset the timer
            }
        }
    }
}
