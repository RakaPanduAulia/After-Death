using System.Collections;
using UnityEngine;

public class HazardFire : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private float damagePerSecond = -1f;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        if (fireParticles != null)
        {
            fireParticles.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            // Define the knockback force (you can adjust these values as necessary)
            Vector2 knockbackForce = new Vector2(5, 5);

            // Create an array to hold the parameters
            object[] parameters = { damagePerSecond, knockbackForce };

            // Apply damage and knockback to the player
            other.gameObject.SendMessage("ApplyDamage", parameters);
        }
    }

    private bool IsPlayer(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & playerLayer) != 0;
    }
}
