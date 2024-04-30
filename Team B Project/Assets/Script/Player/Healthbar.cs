using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public CharacterController2D player;
    private Image healthbar;

    void Awake()
    {
        healthbar = GetComponent<Image>();
    }

    void Update()
    {
        if (player != null)
        {
            healthbar.fillAmount = player.life / 4.0f;
        }
    }
}
