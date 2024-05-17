using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject respawnPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && respawnPanel.activeSelf)
        {
            Time.timeScale = 1f; // Resume the game time
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current level
        }
    }
}
