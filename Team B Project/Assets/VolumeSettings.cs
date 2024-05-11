using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    private Slider volumeSlider;

    public void Start()
    {
        volumeSlider = GetComponent<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);  // Get saved volume or default to 0.5
            volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });
        }
    }

    public void SetVolume(float volume)
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.SetVolume(volume);
            PlayerPrefs.SetFloat("MusicVolume", volume);  // Save volume setting
        }
    }
}
