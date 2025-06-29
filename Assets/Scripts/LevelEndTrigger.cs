using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Triggers end-of-level behavior when the player enters the collider.
/// Can either activate a UI victory window or load a victory scene.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class LevelEndTrigger : MonoBehaviour
{
    [Header("Victory Settings")]
    [Tooltip("Optional UI panel to display on victory. If assigned, scene will not be loaded.")]
    [SerializeField] private GameObject victoryUI;
    
    [Tooltip("Name of the scene to load on victory. Used only if Victory UI is not set.")]
    [SerializeField] private string victorySceneName = "VictoryScene";

    private bool _triggered = false;

    private void Reset()
    {
        // Ensure the collider is set as trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered)
            return;

        if (other.CompareTag("Player"))
        {
            _triggered = true;
            EndLevel();
        }
    }

    /// <summary>
    /// Handles end-of-level logic: shows UI or loads scene.
    /// </summary>
    private void EndLevel()
    {
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            // Optionally pause the game
            Time.timeScale = 0f;
        }
        else
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }
}

