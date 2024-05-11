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
    [SerializeField] private string sceneName;

    private void Start()
    {
        start.SetActive(false);
        exit.SetActive(false);
    }

    private void Update()
    {
        DisableText();
    }

    private void DisableText()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("gasken");

            pressAnyKeyToStart.SetActive(false);

            start.SetActive(true);
            exit.SetActive(true);
        }
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
