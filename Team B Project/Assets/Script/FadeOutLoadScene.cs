using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutLoadScene : MonoBehaviour
{
    [SerializeField] public AnimationClip clip;
    private int indexScene;

    public void LoadScene (int index)
    {
        gameObject.SetActive (true);

        indexScene = index;

        Invoke("FadeOutCheck", clip.length);
    }

    void FadeOutCheck()
    {
        SceneManager.LoadScene (indexScene);
    }
}
