using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TextMeshPro))]
public class SimpleTextPopAnimation : MonoBehaviour, ISimpleAnimation
{
    private TextMeshPro messageText;
    private Sequence showSequence;
    private Sequence hideSequence;
    private Vector3 startingPos;
    private void Awake()
    {
        messageText = GetComponent<TextMeshPro>();
    }

    public void PlayAnimation(Vector3 startingPos, string text)
    {
        this.messageText.text = text;
        this.startingPos = startingPos;
        transform.position = this.startingPos;
        if (showSequence != null && showSequence.IsPlaying())
        {
            showSequence.Kill();
        }
        showSequence.Append(messageText.DOFade(1, 0.5f));
        showSequence.Join(transform.DOMoveY(startingPos.y + 0.5f, 0.5f));
        //showSequence.SetLoops(1, LoopType.Yoyo);
        showSequence.Play();
    }

    public void HideAnimation()
    {
        if (showSequence != null && showSequence.IsPlaying())
        {
            showSequence.Kill();
        }
        showSequence.Append(messageText.DOFade(0, 0.5f));
        showSequence.Join(transform.DOMoveY(startingPos.y, 0.5f));
        showSequence.Play();
    }
    
}
