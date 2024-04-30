using UnityEngine;

public class PauseScene : MonoBehaviour
{
    private string pauseMenuSceneName = "Pause Menu"; // Scene name for the pause menu
    [SerializeField] private string mainGameSceneName = "Level1.1"; // Scene name for the main game

    private bool isPaused = false;
    private ViewManager viewManager;

    void Start()
    {
        // Attempt to get the ViewManager component from the GameObject
        viewManager = GetComponent<ViewManager>();
        if (viewManager == null)
        {
            Debug.LogError("ViewManager component not found on the GameObject");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        if (viewManager == null) return;

        viewManager.PlayScene(pauseMenuSceneName);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (viewManager == null) return;

        viewManager.UnloadScene();
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ReturnToGameScene()
    {
        if (viewManager == null) return;

        // Ensure game is not paused when returning
        Time.timeScale = 1;
        isPaused = false;
        viewManager.ChangeScene(pauseMenuSceneName, mainGameSceneName);
    }
}
