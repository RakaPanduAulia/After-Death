using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    // Create a UnityEvent field
    public UnityEvent onPlayerEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Invoke the event
            onPlayerEnter.Invoke();
        }
    }
}
