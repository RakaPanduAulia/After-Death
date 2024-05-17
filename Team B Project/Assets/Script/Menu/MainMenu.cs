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
    [SerializeField] private Button continueButton;

    [Header("AudioClip")]
    [SerializeField] private AudioClip anyKeyToStart;

    private bool isActive;

    private void Start()
    {
        start.SetActive(false);
        exit.SetActive(false);
        continueButton.gameObject.SetActive(false); // Corrected method call
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
        isActive = true;

        continueButton.interactable = GameManager.instance.HasSaveData();
    }

    private void Update()
    {
        DisableText();
    }

    private void DisableText()
    {
        if (Input.anyKeyDown && isActive)
        {
            pressAnyKeyToStart.SetActive(false);

            AudioController.Instance.PlaySound(anyKeyToStart);

            isActive = false;

            continueButton.gameObject.SetActive(true); // Corrected method call
            start.SetActive(true);
            exit.SetActive(true);
        }
    }

    public void NewGame()
    {
        GameManager.instance.SaveGame("Level1");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        GameManager.instance.LoadGame();
    }
}
