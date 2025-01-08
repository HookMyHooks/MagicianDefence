using UnityEngine;

public class StoneBallCollision : MonoBehaviour
{
    public int damage = 20;

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

