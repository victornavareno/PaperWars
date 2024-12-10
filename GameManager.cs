using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI components
using System.Collections;  // This allows you to use IEnumerator

public class GameManager : MonoBehaviour
{
    private int totalEnemies; // Total number of enemies in the scene
    private int enemiesDefeated; // Number of enemies defeated

    [SerializeField] private GameObject goodJobText; // Reference to the "Good Job" text UI

    void Start()
    {
        // Find all enemies in the scene and count them
        totalEnemies = FindObjectsOfType<EnemyMovement>().Length;
        enemiesDefeated = 0;

        // Ensure the "Good Job" text is hidden at the start
        if (goodJobText != null)
        {
            goodJobText.SetActive(false);
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++; // Increase defeated enemies count

        if (enemiesDefeated >= totalEnemies)
        {
            // All enemies defeated, show "Good Job" and wait for 2 seconds
            StartCoroutine(ShowGoodJobAndWait());
        }
    }

    private IEnumerator ShowGoodJobAndWait()
    {
        // Show the "Good Job" text
        if (goodJobText != null)
        {
            goodJobText.SetActive(true);
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // After 2 seconds, load the next scene
        LoadNextScene();
    }

    void LoadNextScene()
    {
        Debug.Log("Loading next scene...");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there's a next scene
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next scene available.");
        }
    }
}
