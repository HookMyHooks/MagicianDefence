using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    public int health; // Health of the turret

    // Method to handle taking damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Turret took {amount} damage. Remaining health: {health}");

        // Destroy the turret if health drops to zero or below
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Turret destroyed!");
        }
    }
}
