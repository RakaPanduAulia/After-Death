using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;

    private void Start()
    {
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);

        GameManager.instance.SaveGame(SceneManager.GetActiveScene().name);
    }
}
