using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (gameObject.tag == "Turret")
            {
                TurretDestroyed?.Invoke(gameObject); // Notify subscribers
                Destroy(gameObject);
            }
                if (gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void Start()
    {
        if(gameObject.tag == "Turret")
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
