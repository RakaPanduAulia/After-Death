using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Instantiate")]
    [SerializeField] private GameObject pressAnyKeyToStart;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;

    [Header("AudioClip")]
    [SerializeField] private AudioClip anyKeyToStart;

    private bool isActive;

    private void Start()
    {
        start.SetActive(false);
        exit.SetActive(false);
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
        isActive = true;
    }

    private void Update()
    {
        DisableText();
    }

    private void DisableText()
    {
        if (Input.anyKeyDown && isActive == true)
        {
            pressAnyKeyToStart.SetActive(false);

            AudioController.Instance.PlaySound(anyKeyToStart);

            isActive = false;

            start.SetActive(true);
            exit.SetActive(true);
        }
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
