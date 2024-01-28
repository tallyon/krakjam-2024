using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleShineAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        var tweenMove = transform.DOLocalMove(targetPosition, 1f);
        //tweenMove.SetDelay(0.5f);
        tweenMove.SetLoops(-1, LoopType.Restart);
        tweenMove.Play();
    }
}
