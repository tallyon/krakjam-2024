using UnityEngine;
using static SimpleTextPopAnimation;

public class TakeItemInteraction : Interaction
{
    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction  == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null)
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.TwoItems), playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player already have one item");
            }
            else
            {
                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
                Debug.Log($"Interactions: Player has taken item");
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
    }
}
