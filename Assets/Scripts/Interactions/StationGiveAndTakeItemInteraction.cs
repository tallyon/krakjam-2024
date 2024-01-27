using UnityEngine;
using static ItemsData;

public class StationGiveAdnTakeItemInteraction : Interaction
{
    public ItemsEnum giveItemEnum;
    public ItemsEnum takeItemEnum;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == giveItemEnum)
            {
                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = giveItemEnum}, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = takeItemEnum }, playerCharacter.characterTypeEnum);
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You do not have proper item" }, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player tries to interact, does not have proper item");
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}
