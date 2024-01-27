using UnityEngine;
using static ItemsData;

public class InteractableItem : Interactable
{
    [SerializeField] ItemsEnum itemsEnum;

    protected override void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        base.HandleOnInteractionWithObject(interaction, characterTypeEnum);

        if (interaction is TakeItemInteraction)
        {
            var player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);
            player.AddItem(itemsEnum);
            Destroy(gameObject);
        }
    }
}
