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

                Debug.Log($"Interactable item: {characterTypeEnum} has changed sprite of {stationEnum}");
                spriteRenderer.sprite = changeSpriteInteraction.newSprite;
                break;
            case StationGiveItemInteraction giveItemInteraction:
                Debug.Log($"Interactable item: {characterTypeEnum} has taken {giveItemInteraction.giveItemEnum} to {stationEnum}");
                player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);
                player.DeleteItem();
                break;
            case StationTakeItemInteraction stationTakeItemInteraction:
                Debug.Log($"Interactable item: {characterTypeEnum} has taken {stationTakeItemInteraction.takeItemEnum} from {stationEnum}");
                player.AddItem(stationTakeItemInteraction.takeItemEnum);
                break;
            case DoorInteraction doorInteraction:
                Debug.Log($"Interactable item: {characterTypeEnum} has interacted with  door");
                break;
        }
    }
}
