using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    //Buat variabel untuk mengecek apakah sedang rewind atau tidak
    public bool isRewinding = false;

    //Buat list untuk menyimpan posisi
    List<Vector3> positions;

    //Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Inisialisasi list
        positions = new List<Vector3>();
        //rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Jika sedang rewind, panggil fungsi rewind dan jika tidak panggil fungsi record
        if(isRewinding)
            Rewind();
        else
            Record();
    }

    //Fungsi untuk merekam posisi
    void Record()
    {
        positions.Insert(0, transform.position);
    }

    //Fungsi untuk rewind
    void Rewind() 
    {
        if (positions.Count > 0) 
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Jika tombol space ditekan, panggil fungsi StartRewind dan jika dilepas panggil fungsi StopRewind
        if(Input.GetKeyDown(KeyCode.Space))
            StartRewind();
        if(Input.GetKeyUp(KeyCode.Space))
            StartRewind();
    }

    //Fungsi untuk memulai rewind
    public void StartRewind()
    {
        isRewinding = true;
        //rb.isKinematic = true;
    }

    //Fungsi untuk menghentikan rewind
    public void StopRewind()
    {
        isRewinding = false;
        //rb.isKinematic = false;
    } 
}
