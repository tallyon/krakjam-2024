using UnityEngine;

public class InteractableItem : Interactable
{
    [SerializeField] ItemsEnum itemsEnum;

    protected override void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        base.HandleOnInteractionWithObject(interaction, characterTypeEnum);

        if (interaction is TakeItemInteraction takeItemInteraction)
        {
            var player = _gameStateController.GetPlayerObject(characterTypeEnum);
            player.AddItem(itemsEnum);
            Destroy(gameObject);
        }
    }
}

public enum ItemsEnum
{
    Cake = 0,
    Pork =1
}
