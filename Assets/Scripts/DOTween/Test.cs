using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text tmp_text;

    [SerializeField] private Vector3 scaleStart;
    [SerializeField] private Vector3 scaleEnd;
    [SerializeField] private float duration;
    [SerializeField] private float maxY;
    [SerializeField] private int money;

    void Start()
    {
        tmp_text.text = money.ToString();
        rectTransform.DOMoveY(maxY, duration);
        // var moveTween = rectTransform.DOMoveY(maxY, duration);
        // var scaleTween = rectTransform.DOScale(scaleEnd, .4f)
        //     .SetLoops(-1, LoopType.Yoyo);
        //
        // moveTween.OnComplete(() => scaleTween.Kill());
    }
}
