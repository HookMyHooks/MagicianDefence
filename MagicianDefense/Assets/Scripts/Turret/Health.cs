using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;

    public delegate void OnDestroyed(GameObject destroyedObject);
    public static event OnDestroyed TurretDestroyed;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            TurretDestroyed?.Invoke(gameObject); // Notify subscribers
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TurretManager.Instance.RegisterTurret(gameObject);
    }

    private void OnDestroy()
    {
        if (TurretManager.Instance != null)
        {
            TurretManager.Instance.UnregisterTurret(gameObject);
        }
    }



}
