using UnityEngine;
using static ItemsData;

public class InteractableItem : Interactable
{
    [SerializeField] ItemsEnum itemsEnum;

    private void Awake()
    {
        _simpleTextPopAnimationMiddle = GetComponentInChildren<SimpleTextPopAnimation>();
    }

    protected override void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        base.HandleOnInteractionWithObject(interaction, characterTypeEnum);
        Debug.Log(itemsEnum + " on interaction");

        if (interaction is TakeItemInteraction)
        {
            Debug.Log($"Interactable item: {characterTypeEnum} has taken {itemsEnum}");

            var player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);
            player.AddItem(itemsEnum);
            Destroy(gameObject);
        }
    }
}
