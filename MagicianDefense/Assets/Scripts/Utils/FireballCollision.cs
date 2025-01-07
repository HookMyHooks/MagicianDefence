  using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireballCollision : MonoBehaviour
{
    [Header("References")]
    public int damage; // Damage dealt by the fireball

    private void OnTriggerEnter(Collider collision)
    {
      
        // Check if the object hit has the "Turret" component
        //Turret turret = collision.gameObject.GetComponent<Turret>();
        
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
}
