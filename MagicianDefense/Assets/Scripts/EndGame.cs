using UnityEngine;
using UnityEngine.SceneManagement;

public class MinionChecker : MonoBehaviour
{
    [Header("Check Interval")]
    [Tooltip("How often to check for remaining minions (in seconds).")]
    public float checkInterval = 10f;

    [Header("Target Scene")]
    [Tooltip("The name of the scene to load when all minions are gone.")]
    public string winSceneName = "Winn";

    private void Start()
    {
        // Start the repeating check
        InvokeRepeating(nameof(CheckForMinions), checkInterval, checkInterval);
    }

    private void CheckForMinions()
    {
        // Find all GameObjects tagged as "Minion"
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");

        // Log the number of minions found
        Debug.Log($"MinionChecker: {minions.Length} minions remaining on the map.");

        // If no minions are left, load the win scene
        if (minions.Length == 0)
        {
            Debug.Log("No minions left! Loading win scene...");
            SceneManager.LoadScene(winSceneName);
        }
    }

    private void OnDisable()
    {
        // Stop the repeating invocation when the script is disabled
        CancelInvoke(nameof(CheckForMinions));
    }
}
