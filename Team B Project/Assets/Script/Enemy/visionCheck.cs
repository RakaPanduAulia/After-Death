using UnityEngine;

public class visionCheck : MonoBehaviour
{
    public LayerMask playerLayer; // Set this to the layer the player is on
    public bool isInVision;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is on the player layer
        if (1 << other.gameObject.layer == playerLayer.value)
        {
            Debug.Log("Player entered vision");
            isInVision = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object is on the player layer
        if (1 << other.gameObject.layer == playerLayer.value)
        {
            Debug.Log("Player exited vision");
            isInVision = false;
        }
    }
}
