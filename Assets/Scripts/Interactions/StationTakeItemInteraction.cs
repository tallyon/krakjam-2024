using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class StationTakeItemInteraction : Interaction
{
    public ItemsEnum takeItemEnum;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {


        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null)
            {

                Debug.Log($"Interactions: Player cannot take second item");

                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.TwoItems), playerCharacter.characterTypeEnum);
            }
            else
            {

                Debug.Log($"Interactions: Player has taken an item ");

                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            }
        }
        else
        {

            Debug.Log($"Interactions: Player cannot interact with this item with this character");

            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
        }
    }
}
