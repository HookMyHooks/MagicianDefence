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
        
        if(collision.CompareTag("Minion"))
        {
            Minion enemyMinion = GameObject.FindAnyObjectByType<Minion>();

            enemyMinion.TakeDamage(damage);
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

       
    }
}
