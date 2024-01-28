using UnityEngine;
using static ItemsData;

public class StationGiveItemInteraction : Interaction
{
    public ItemsEnum giveItemEnum;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            return;
        }

        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == giveItemEnum)
            {

                Debug.Log($"Interactions: Player has given item");

                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
                _wasUsed = true;

            }
            else
            {

                Debug.Log($"Interactions: Player does not have proper item to give");

                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You do not have proper item" }, playerCharacter.characterTypeEnum);
            }
        }
        else
        {

            Debug.Log($"Interactions: Player cannot interact with this item ith this character");

            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
        }
    }
}
