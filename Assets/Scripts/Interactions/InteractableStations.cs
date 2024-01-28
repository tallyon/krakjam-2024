using System;
using System.Threading.Tasks;
using UnityEngine;
using static ItemsData;
using static StationsData;
using static UnityEngine.ParticleSystem;

public class InteractableStations : Interactable
{
    [SerializeField] SimpleTextPopAnimation _simpleTextPopAnimationLeft;
    [SerializeField] SimpleTextPopAnimation _simpleTextPopAnimationRight;

    [SerializeField] StationEnum stationEnum;
    public StationEnum StationEnum => stationEnum;
    [SerializeField] SpriteRenderer spriteRenderer;

    protected async override void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        base.HandleOnInteractionWithObject(interaction, characterTypeEnum);

        var player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);
        await WaitForInteraction(interaction, player);
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
            case VentPathInteraction ventPathInteraction:
                Debug.Log($"Interactable item: {characterTypeEnum} has taken vent from");
                break;
            case StationOptionInteraction stationOptionInteraction:
                Debug.Log($"Player has 3 items to get: {stationOptionInteraction.takeItemEnum1} {stationOptionInteraction.takeItemEnum2} {stationOptionInteraction.takeItemEnum3}");
                OnSpecialInteractionPerformed?.Invoke(stationOptionInteraction);
                break;
            case DoorInteraction doorInteraction:
                Debug.Log($"Interactable item: {characterTypeEnum} has interacted with  door");
                break;
            case TrophyCaseInteraction trophyCaseInteraction:
                Debug.Log($"Interactable item: {characterTypeEnum} has interacted with  door");
                break;
        }
    }

    private async Task WaitForInteraction(Interaction interaction, PlayerCharacter player)
    {
        if (interaction.InteractionTimeMS > 0)
        {
            player.ApplyStatus(PlayerCharacterStatus.Interacting);
            player.StartInteracting(interaction.InteractionTimeMS);
            particle.Play();
            await Task.Delay(interaction.InteractionTimeMS);
            particle.Stop();
            player.ApplyStatus(PlayerCharacterStatus.Normal);
        }
    }

    public void ShowChoices(ItemsEnum itemsEnum1, ItemsEnum itemsEnum2, ItemsEnum itemsEnum3)
    {
        _simpleTextPopAnimationLeft.Setup(SimpleTextPopAnimation.InfoEnums.SimpleItem, itemsEnum1);
        _simpleTextPopAnimationMiddle.Setup(SimpleTextPopAnimation.InfoEnums.SimpleItem, itemsEnum2);
        _simpleTextPopAnimationRight.Setup(SimpleTextPopAnimation.InfoEnums.SimpleItem, itemsEnum3);

        _simpleTextPopAnimationLeft.PlayAnimation(transform.position);
        _simpleTextPopAnimationMiddle.PlayAnimation(transform.position);
        _simpleTextPopAnimationRight.PlayAnimation(transform.position);
    }

    public void HideChoises()
    {
        _simpleTextPopAnimationLeft.HideAnimation();
        _simpleTextPopAnimationMiddle.HideAnimation();
        _simpleTextPopAnimationRight.HideAnimation();
    }
}


