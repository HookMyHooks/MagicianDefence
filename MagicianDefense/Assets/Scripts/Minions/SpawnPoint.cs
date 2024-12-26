using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minionPrefab;  // Reference to the minion prefab
    public float spawnInterval = 2f; // Delay between spawns
    public int maxMinions = 6;      // Maximum number of minions to spawn

    private int minionsSpawned = 0;  // Counter for spawned minions

    void Start()
    {

        StartCoroutine(SpawnMinions());
    }

    private System.Collections.IEnumerator SpawnMinions()
    {
        while (minionsSpawned < maxMinions)
        {
            SpawnMinion();
            minionsSpawned++;
            yield return new WaitForSeconds(spawnInterval); // Wait for the interval before spawning the next minion
        }
    }

    private void SpawnMinion()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Instantiate(minionPrefab, transform.position + randomOffset, transform.rotation);
    }

}
