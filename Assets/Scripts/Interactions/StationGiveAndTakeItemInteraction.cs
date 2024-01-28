using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class StationGiveAdnTakeItemInteraction : Interaction
{
    public ItemsEnum giveItemEnum;
    public ItemsEnum takeItemEnum;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum);
            return;
        }

        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == giveItemEnum)
            {
                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = giveItemEnum}, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = takeItemEnum }, playerCharacter.characterTypeEnum);
                _wasUsed = true;
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoItem, giveItemEnum), playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player tries to interact, does not have proper item");
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}
