using UnityEngine;

public class TakeItemInteraction : Interaction
{
    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(possiblePlayerInteraction  == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null)
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You cannot have 2 collected items" }, playerCharacter.characterTypeEnum);
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
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Item cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
    }
}
