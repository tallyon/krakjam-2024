using System.Collections.Generic;
using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class TrophyCaseInteraction : Interaction
{
    public Sprite newSprite;
    public ItemsEnum itemEnum;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum);
            return;
        }

        if (playerCharacter.characterTypeEnum == CharacterTypeEnum.Sigma)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.ChadAbility), playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
    }

    public override List<Vector2> PlayAbility(PlayerCharacter playerCharacter)
    {
        if (playerCharacter.characterTypeEnum == CharacterTypeEnum.Sigma)
        {
            OnInteraction?.Invoke(new StationChangeSpriteInteraction() { newSprite = newSprite }, CharacterTypeEnum.Sigma);
            OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = itemEnum}, CharacterTypeEnum.Sigma);
            Debug.Log("Interactions: Player already have one item");

            return new List<Vector2>() { (Vector2)this.gameObject.transform.position };
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
            return null;
        }
    }
}
