using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager : MonoBehaviour
{
    private Stack<string> loadedScenes = new Stack<string>();

    public void PlayScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadedScenes.Push(sceneName); // Push the loaded scene to the stack
    }

    public void UnloadScene()
    {
        if (loadedScenes.Count > 0)
        {
            string sceneName = loadedScenes.Pop(); // Pop the last loaded scene from the stack
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    // Added method to unload a specific scene and load another one
    public void ChangeScene(string sceneToUnload, string sceneToLoad)
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }
}
