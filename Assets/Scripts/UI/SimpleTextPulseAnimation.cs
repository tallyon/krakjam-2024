using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SimpleTextPulseAnimation : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private RectTransform rectTransform;
    

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        //var scaleTween = rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f);
        var textColor = textMesh.DOColor(Color.gray, 1f);
        //scaleTween.SetLoops(-1, LoopType.Yoyo);
        textColor.SetLoops(-1, LoopType.Yoyo);
    }

    
}
