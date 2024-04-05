using UnityEngine;

public class DeathRewindDisplay : MonoBehaviour
{
    public GameObject panel; // Panel to display when the player is dead
    public GameObject rewindPanel; // Panel to display during rewinding

    // Method to update the UI based on the player state
    public void UpdateDisplay(bool isDead, bool isRewinding)
    {
        panel.SetActive(isDead);
        rewindPanel.SetActive(isRewinding);
    }
}
