using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void UnloadScene()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("Pause Menu");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
}
