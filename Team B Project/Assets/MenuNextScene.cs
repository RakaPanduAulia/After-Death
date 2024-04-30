using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes
using DG.Tweening; // Required for DOTween

public class MenuNextScene : MonoBehaviour
{
    public string nextSceneName = "IntroCutscene"; // Name of the next scene to load

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, display a message or do other initialization
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown) // anyKeyDown is true in the frame a key is pressed
        {
            DOTween.KillAll(); // Stop all tweens

            // Load the specified scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
