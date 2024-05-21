using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastBoss : MonoBehaviour
{
    // Nama scene yang akan dipindah
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnDestroy dipanggil ketika GameObject dihancurkan
    void OnDestroy()
    {
        // Pastikan bahwa GameObject benar-benar dihancurkan dalam kondisi runtime
        if (gameObject != null)
        {
            // Pindah ke scene yang ditentukan
            SceneManager.LoadScene(sceneName);
        }
    }
}
