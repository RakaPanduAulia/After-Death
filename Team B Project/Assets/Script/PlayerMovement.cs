using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerakan pemain

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Mendapatkan input horizontal (A/D, Left Arrow/Right Arrow)
        float moveX = Input.GetAxis("Horizontal");

        // Membuat vektor gerakan dengan kecepatan
        Vector3 movement = new Vector3(moveX, 0f, 0f) * moveSpeed * Time.deltaTime;

        // Menggerakkan pemain
        transform.position += movement;
    }
}
