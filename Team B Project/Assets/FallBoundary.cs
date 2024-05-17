using UnityEngine;

public class FallBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterController2D player = collision.GetComponent<CharacterController2D>();
            if (player != null)
            {
                player.FallDeath();
            }
        }
    }
}
