using UnityEngine;

public class Minion : MonoBehaviour
{
    [Header("References")]
    public int health; // Health of the minion

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
