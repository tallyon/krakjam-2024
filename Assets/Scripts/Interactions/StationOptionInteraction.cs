using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class StationOptionInteraction : Interaction
{
    public ItemsEnum takeItemEnum1;
    public ItemsEnum takeItemEnum2;
    public ItemsEnum takeItemEnum3;


    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if(playerCharacter.collectedItem != null)
            {
                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.TwoItems), playerCharacter.characterTypeEnum);
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}
