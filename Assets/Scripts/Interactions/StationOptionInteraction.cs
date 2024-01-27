using UnityEngine;
using static ItemsData;

public class StationOptionInteraction : Interaction
{
    public ItemsEnum takeItemEnum1;
    public ItemsEnum takeItemEnum2;
    public ItemsEnum takeItemEnum3;


    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be interacted by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}
