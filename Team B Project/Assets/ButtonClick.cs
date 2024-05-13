using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    public AudioClip customButtonSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (customButtonSound != null)
        {
            AudioController.Instance.PlaySound(customButtonSound);
        }

        AudioController.Instance.PlayButtonClickSound();
    }
}
