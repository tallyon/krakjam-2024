using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class StationOptionGiveAdnTakeItemInteraction : Interaction
{
    public ItemsEnum giveItemEnum1;
    public ItemsEnum takeItemEnum1;

    public ItemsEnum giveItemEnum2;
    public ItemsEnum takeItemEnum2;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum);
            return;
        }

        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null)
            {
                if (playerCharacter.collectedItem.itemsEnum == giveItemEnum1)
                {
                    Debug.Log($"Interactions: Player has given item");
                    OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = giveItemEnum1 }, playerCharacter.characterTypeEnum);
                    Debug.Log($"Interactions: Player has given item");
                    OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = takeItemEnum1 }, playerCharacter.characterTypeEnum);
                    _wasUsed = true;
                }
                else if (playerCharacter.collectedItem.itemsEnum == giveItemEnum2)
                {
                    Debug.Log($"Interactions: Player has given item");
                    OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = giveItemEnum2 }, playerCharacter.characterTypeEnum);
                    Debug.Log($"Interactions: Player has given item");
                    OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = takeItemEnum2 }, playerCharacter.characterTypeEnum);
                    _wasUsed = true;
                }
                else
                {
                    OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoItem, giveItemEnum1), playerCharacter.characterTypeEnum);
                    Debug.Log($"Interactions: Player tries to interact, does not have proper item");
                }
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoItem), playerCharacter.characterTypeEnum);
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
