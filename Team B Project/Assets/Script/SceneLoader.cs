using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Name of the scene to load

    public void LoadScene()
    {
        // Start the coroutine to wait for a second before loading the scene
        SceneManager.LoadScene(sceneName);
    }
}
