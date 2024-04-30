using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    public void LoadNextScene()
    {
        // Start the coroutine to wait for a second before loading the scene
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        // Wait for one second
        yield return new WaitForSeconds(3f);

        // Load the specified scene
        SceneManager.LoadScene(nextSceneName);
    }
}
