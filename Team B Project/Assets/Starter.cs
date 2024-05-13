using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;

    private void Start()
    {
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
    }
}
