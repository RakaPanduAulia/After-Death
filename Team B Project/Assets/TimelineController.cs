using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;
    public string nextSceneName = "IntroDialogue"; // Replace with your scene name

    void Start()
    {
        // Ensure that the PlayableDirector component is referenced
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }

        // Subscribe to the event that is called when the timeline finishes playing
        director.stopped += OnPlayableDirectorStopped;
    }

    // Event handler for when the timeline stops
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnDestroy()
    {
        // Always unsubscribe from the event when the object is destroyed
        director.stopped -= OnPlayableDirectorStopped;
    }
}
