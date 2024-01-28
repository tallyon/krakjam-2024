using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using static ItemsData;
using UnityEngine.UI;
using System.Threading.Tasks;
using UI;

public class SimpleTextPopAnimation : MonoBehaviour, ISimpleAnimation
{
    [SerializeField] InfoBubbleConfig infoConfig;
    [SerializeField] Vector2 newPostition = new Vector2(0, 0.5f);
    [SerializeField] float animationTime = 0.5f;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image parentImage;
    [SerializeField] private Image inputIcon;
    [SerializeField] private Image inputIcon2;

    [SerializeField]
    private InputIconsConfig inputIcons;

    [SerializeField] private CanvasGroup _canvasGroup;
    //private SpriteRenderer spriteRenderer;
    private Sequence showSequence;
    private Sequence hideSequence;
    private Vector3 startingPos;
    private GameObject _spawnedObject;
    private bool isAnimInProgress = false;

    private InputIconSet keyboardIcons;
    private InputIconSet gamepadIcons;
    
    // private SpriteRenderer spriteRendererChild;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(InfoEnums infoEnums, ItemsEnum itemEnums)
    {
        keyboardIcons = inputIcons.GetIcons("FastKeyboard"); 
        gamepadIcons = inputIcons.GetIcons("");
        parentImage.sprite = infoConfig.GetCollectedItemPrefab(infoEnums);

        if (infoEnums == InfoEnums.SimpleItem || infoEnums == InfoEnums.NoItem)
        {
            if(infoEnums == InfoEnums.NoItem)
            {
                itemImage.color = Color.black;
            }
            else
            {
                itemImage.color = Color.white;
            }
            var item = GameStateController.Instance.GetCollectedItemPrefab(itemEnums);
            itemImage.sprite = item.itemSprite;
        }
        else
        {
            itemImage.enabled = false;
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
        if (newPostition.x < 0)
        {
            inputIcon.sprite = keyboardIcons.icons.interact1;
            inputIcon2.sprite = gamepadIcons.icons.interact1;
        }
        else if(newPostition.x == 0)
        {

            inputIcon.sprite = keyboardIcons.icons.interact2;
            inputIcon2.sprite = gamepadIcons.icons.interact2;

        }
        else if(newPostition.x > 0)
        {
            inputIcon.sprite = keyboardIcons.icons.interact3;
            inputIcon2.sprite = gamepadIcons.icons.interact3;

        }
        showSequence.OnComplete(async () =>
        {
            await Task.Delay(2);
            HideAnimation();
        });
        
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
        TwoItems,
        NerdAbilityDoors
    }
    
}
