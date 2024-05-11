using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioClip[] musicClips;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBySceneName(scene.name);
    }

    private void PlayMusicBySceneName(string sceneName)
    {
        int clipIndex = -1;

        // Determine which clip to play based on the scene name
        switch (sceneName)
        {
            case "Main Menu":
                clipIndex = 0;
                break;
            case "IntroCutscene":
                clipIndex = 1;
                break;
            case "Level1.1":
                clipIndex = 2;
                break;
                // Add more cases as needed for each scene
        }

        if (clipIndex != -1 && clipIndex < musicClips.Length)
        {
            audioSource.clip = musicClips[clipIndex];
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
