using UnityEngine;
using static ItemsData;

public class StationGiveItemInteraction : Interaction
{
    public ItemsEnum giveItemEnum;

    public override bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == giveItemEnum)
            {

                Debug.Log($"Interactions: Player has given item");

                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
                return true;
            }
            else
            {

                Debug.Log($"Interactions: Player does not have proper item to give");

                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You do not have proper item" }, playerCharacter.characterTypeEnum);
                return false;
            }
        }
        else
        {

            Debug.Log($"Interactions: Player cannot interact with this item ith this character");

            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            return false;
        }
    }
}
