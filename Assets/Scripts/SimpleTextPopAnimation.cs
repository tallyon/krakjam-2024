using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class SimpleTextPopAnimation : MonoBehaviour, ISimpleAnimation
{
    [SerializeField] Vector2 newPostition = new Vector2(0, 0.5f);
    [SerializeField] float animationTime = 0.5f;
    [SerializeField] private TextMeshPro messageText;
    private SpriteRenderer spriteRenderer;
    private Sequence showSequence;
    private Sequence hideSequence;
    private Vector3 startingPos;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        showSequence.Append(messageText.DOFade(1, animationTime));
        showSequence.Join(spriteRenderer.DOFade(1, animationTime));
        showSequence.Join(transform.DOMove(new Vector3(startingPos.x + newPostition.x, startingPos.y + newPostition.y, 0), animationTime));
        //showSequence.SetLoops(1, LoopType.Yoyo);
        showSequence.Play();
    }

    public void HideAnimation()
    {
        if (showSequence != null && showSequence.IsPlaying())
        {
            showSequence.Kill();
        }
        showSequence.Append(messageText.DOFade(0, animationTime));
        showSequence.Join(transform.DOMoveY(startingPos.y, animationTime));
        showSequence.Join(spriteRenderer.DOFade(0, animationTime));
        showSequence.Play();

        
    }
    
}
