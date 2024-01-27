using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StationChangeSpriteInteraction : Interaction
{
    [SerializeField] List<ChangeStationSpriteData> sprites;

    public Sprite newSprite { get; set; }

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            newSprite = sprites.FirstOrDefault(x => x.characterEnum == playerCharacter.characterTypeEnum).sprite;

            OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player chnged sprite of item");
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}

[Serializable]
public class ChangeStationSpriteData
{
    public Sprite sprite;
    public CharacterTypeEnum characterEnum;
}