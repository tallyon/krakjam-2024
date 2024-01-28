using UnityEngine;
using static ItemsData;

public class StationGiveItemOptionInteraction : Interaction
{
    public ItemsEnum giveItem;

    public ItemsEnum takeItemEnum1;
    public ItemsEnum takeItemEnum2;
    public ItemsEnum takeItemEnum3;


    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if(playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == giveItem)
            {
                OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = giveItem }, playerCharacter.characterTypeEnum);
                OnInteraction?.Invoke(new StationOptionInteraction() { takeItemEnum1= takeItemEnum1 , takeItemEnum2 = takeItemEnum2 , takeItemEnum3 = takeItemEnum3 }, playerCharacter.characterTypeEnum);
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player cannot interact with this item with this character");
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}
