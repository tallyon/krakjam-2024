using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using static ItemsData;
using UnityEngine.UI;

public class SimpleTextPopAnimation : MonoBehaviour, ISimpleAnimation
{
    [SerializeField] InfoBubbleConfig infoConfig;
    [SerializeField] Vector2 newPostition = new Vector2(0, 0.5f);
    [SerializeField] float animationTime = 0.5f;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image parentImage;

    [SerializeField] private CanvasGroup _canvasGroup;
    //private SpriteRenderer spriteRenderer;
    private Sequence showSequence;
    private Sequence hideSequence;
    private Vector3 startingPos;
    private GameObject _spawnedObject;
   // private SpriteRenderer spriteRendererChild;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(InfoEnums infoEnums, ItemsEnum itemEnums)
    {
        var infoObj  = infoConfig.GetCollectedItemPrefab(infoEnums);
        //_spawnedObject = Instantiate(infoObj, transform.parent);
        //spriteRendererChild = _spawnedObject.GetComponentInChildren<SpriteRenderer>();

        if (infoEnums == InfoEnums.SimpleItem)
        {
            var item = GameStateController.Instance.GetCollectedItemPrefab(itemEnums);
            itemImage.sprite = item.itemSprite;
            //spriteRendererChild.sprite = item.itemSprite;
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

        showSequence.Append(parentImage.DOFade(1, animationTime));
        showSequence.Join(itemImage.DOFade(1, animationTime));
        showSequence.Join(_canvasGroup.DOFade(1, animationTime));
        showSequence.Join(transform.DOMove(new Vector3(startingPos.x + newPostition.x, startingPos.y + newPostition.y, 0), animationTime));

        showSequence.Play();
    }

    public void HideAnimation()
    {
        if (showSequence != null && showSequence.IsPlaying())
        {
            showSequence.Kill();
        }

        showSequence.Append(transform.DOMove(startingPos, animationTime));
        showSequence.Join(itemImage.DOFade(0, animationTime));
        showSequence.Join(parentImage.DOFade(0, animationTime));
        showSequence.Join(_canvasGroup.DOFade(0, animationTime));
        showSequence.Play();
    }

    public enum InfoEnums
    {
        NoInteraction,
        SimpleItem,
        NoItem,
        Used,
        ChadAbility,
        NerdAbility,
        TwoItems
    }
    
}
