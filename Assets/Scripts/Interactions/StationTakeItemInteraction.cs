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
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You cannot have 2 collected items" }, playerCharacter.characterTypeEnum);
                return false;
            }
            else
            {
                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
                return true;
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            return false;
        }
    }
}
