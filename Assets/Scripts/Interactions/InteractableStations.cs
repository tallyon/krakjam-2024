using UnityEngine;
using static StationsData;

public class InteractableStations : Interactable
{
    [SerializeField] StationEnum stationEnum;
    [SerializeField] SpriteRenderer spriteRenderer;

    protected override void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        base.HandleOnInteractionWithObject(interaction, characterTypeEnum);

        var player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);

        switch (interaction)
        {
            case StationChangeSpriteInteraction changeSpriteInteraction:
                spriteRenderer.sprite = changeSpriteInteraction.newSprite;
                break;
            case StationGiveItemInteraction giveItemInteraction:
                player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);
                player.DeleteItem();
                break;
            case StationTakeItemInteraction stationTakeItemInteraction:
                player.AddItem(stationTakeItemInteraction.takeItemEnum);
                break;
            case DoorInteraction doorInteraction:
                break;
        }
    }
}
