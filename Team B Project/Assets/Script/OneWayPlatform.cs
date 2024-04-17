using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime = 1f;  // Waktu tunggu sebelum platform dapat dilewati kembali

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        // Jika pemain menekan tombol turun (misalnya, panah bawah)
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DropThrough());
        }
    }

    private System.Collections.IEnumerator DropThrough()
    {
        // Mengabaikan tabrakan platform
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(waitTime);
        effector.rotationalOffset = 0f;  // Mengembalikan keadaan normal platform
    }
}
