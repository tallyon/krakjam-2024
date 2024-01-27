using UnityEngine;
using static ItemsData;

public class GirlGiveItemInteraction : Interaction
{
    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
            if (playerCharacter.collectedItem != null)
            {

                Debug.Log($"Interactions: Player has given item");
                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            }
            else
            {

                Debug.Log($"Interactions: Player does not have any item to give");

                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You do not have any item" }, playerCharacter.characterTypeEnum);
            }
    }
}
