  using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireballCollision : MonoBehaviour
{
    [Header("References")]
    public int damage; // Damage dealt by the fireball

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            Minion minion = other.GetComponent<Minion>();
            if (minion != null)
            {
                minion.TakeDamage(damage);
                Debug.Log($"Minion hit! Damage dealt: {damage}");
                Destroy(gameObject); // Destroy the stone ball upon collision
            }
        }
    }
}
