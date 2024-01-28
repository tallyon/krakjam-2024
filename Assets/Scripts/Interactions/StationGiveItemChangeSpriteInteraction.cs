using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class StationGiveItemChangeSpriteInteraction : Interaction
{
    [SerializeField] List<ChangeStationSpriteData> sprites;

    public ItemsEnum _giveItemEnum;
    public Sprite _newSprite { get; set; }
    
    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum);
            return;
        }

        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == _giveItemEnum)
            {

                Debug.Log($"Interactions: Player has given item");

                OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = _giveItemEnum }, playerCharacter.characterTypeEnum);

                _newSprite = sprites.FirstOrDefault(x => x.characterEnum == playerCharacter.characterTypeEnum).sprite;
                OnInteraction?.Invoke(new StationChangeSpriteInteraction() { newSprite = _newSprite }, playerCharacter.characterTypeEnum);
                _wasUsed = true;
            }
            else
            {
                Debug.Log($"Interactions: Player does not have proper item to give");

                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoItem, _giveItemEnum), playerCharacter.characterTypeEnum);
            }

            
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log($"Interactions: Player cannot interact with this item with this character");
        }
    }
}