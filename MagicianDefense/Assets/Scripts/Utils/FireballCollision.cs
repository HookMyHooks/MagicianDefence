using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireballCollision : MonoBehaviour
{
    [Header("References")]
    public int damage; // Damage dealt by the fireball

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object hit has the "Turret" component
        Turret turret = collision.gameObject.GetComponent<Turret>();
        if (turret != null)
        {
            // Apply damage to the turret
            turret.TakeDamage(damage);

            // Optionally destroy the fireball on impact
            Destroy(gameObject);
        }
    }
}
