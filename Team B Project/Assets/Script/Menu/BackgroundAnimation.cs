using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // canvasGroup.DOFade(0f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalMoveX(110f, 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }


}
