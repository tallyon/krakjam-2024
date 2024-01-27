using UnityEngine;
using static ItemsData;

public class StationGiveAdnTakeItemInteraction : Interaction
{
    public ItemsEnum giveItemEnum;
    public ItemsEnum takeItemEnum;

    public override bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == giveItemEnum)
            {
                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = giveItemEnum}, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = takeItemEnum }, playerCharacter.characterTypeEnum);
                return true;
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You do not have proper item" }, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player tries to interact, does not have proper item");
                return false;
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
            return false;
        }
    }
}
