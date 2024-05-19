using System.Collections;
using UnityEngine;

public class HazardFire : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private float damagePerSecond = 1f;
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
        
    }

    private bool IsPlayer(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & playerLayer) != 0;
    }
}
