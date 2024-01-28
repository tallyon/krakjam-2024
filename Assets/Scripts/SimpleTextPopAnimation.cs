using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using static ItemsData;

public class SimpleTextPopAnimation : MonoBehaviour, ISimpleAnimation
{
    [SerializeField] InfoBubbleConfig infoConfig;
    [SerializeField] Vector2 newPostition = new Vector2(0, 0.5f);
    [SerializeField] float animationTime = 0.5f;
    private SpriteRenderer spriteRenderer;
    private Sequence showSequence;
    private Sequence hideSequence;
    private Vector3 startingPos;
    private GameObject _spawnedObject;
    private SpriteRenderer spriteRendererChild;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(InfoEnums infoEnums, ItemsEnum itemEnums)
    {
        var infoObj  = infoConfig.GetCollectedItemPrefab(infoEnums);
        _spawnedObject = Instantiate(infoObj, transform);
        spriteRendererChild = _spawnedObject.GetComponentInChildren<SpriteRenderer>();

        if (infoEnums == InfoEnums.SimpleItem)
        {
            var item = GameStateController.Instance.GetCollectedItemPrefab(itemEnums);
            spriteRendererChild.sprite = item.itemSprite;
        }
    }

    public void PlayAnimation(Vector3 startingPos)
    {
        this.startingPos = startingPos;
        transform.position = this.startingPos;
        if (showSequence != null && showSequence.IsPlaying())
        {
            showSequence.Kill();
        }

        showSequence.Append(spriteRenderer.DOFade(1, animationTime));
        showSequence.Join(spriteRendererChild.DOFade(1, animationTime));
        showSequence.Join(transform.DOMove(new Vector3(startingPos.x + newPostition.x, startingPos.y + newPostition.y, 0), animationTime));

        showSequence.Play();
    }

    public void HideAnimation()
    {
        if (showSequence != null && showSequence.IsPlaying())
        {
            showSequence.Kill();
        }

        showSequence.Append(transform.DOMoveY(startingPos.y, animationTime));
        showSequence.Join(spriteRendererChild.DOFade(0, animationTime));
        showSequence.Join(spriteRenderer.DOFade(0, animationTime));
        showSequence.Play();
    }

    public enum InfoEnums
    {
        NoChad,
        NoNerd,
        SimpleItem,
        NoItem
    }
    
}
