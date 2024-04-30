using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        // Use DOFade method to fade the CanvasGroup
        canvasGroup.DOFade(0f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
