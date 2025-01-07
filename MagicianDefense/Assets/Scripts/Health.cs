using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Debug.Log($"{gameObject.name} destroyed!");
            Destroy(gameObject);
        }
    }
}
