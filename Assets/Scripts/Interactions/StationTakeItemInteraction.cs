using UnityEngine;
using static ItemsData;

public class StationTakeItemInteraction : Interaction
{
    public ItemsEnum takeItemEnum;

    public override bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null)
            {

                Debug.Log($"Interactions: Player cannot take second item");

                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You cannot have 2 collected items" }, playerCharacter.characterTypeEnum);
                return false;
            }
            else
            {

                Debug.Log($"Interactions: Player has taken an item ");

                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
                return true;
            }
        }
        else
        {

            Debug.Log($"Interactions: Player cannot interact with this item with this character");

            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            return false;
        }
    }
}
