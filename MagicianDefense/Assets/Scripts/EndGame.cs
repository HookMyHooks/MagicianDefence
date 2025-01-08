using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    private Health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();    
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion").;
        if(minions == null)
        {
            SceneManager.LoadScene("Winn");
        }

        if(health.health <=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}
