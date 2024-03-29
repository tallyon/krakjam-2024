
using UnityEngine;
using static ItemsData;
using static SimpleTextPopAnimation;

public class StationGiveItemChangeSpriteTakeItemInteraction : Interaction
{
    public ItemsEnum _giveItemEnum;
    public ItemsEnum _takeItemEnum;
    public Sprite _newSprite;
    public Sprite _lastSprite;

    private bool wasWatered = false;
    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum); return;
        }

        if (possiblePlayerInteraction == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (!wasWatered && playerCharacter.collectedItem != null && playerCharacter.collectedItem.itemsEnum == _giveItemEnum)
            {

                Debug.Log($"Interactions: Player has given item");
                wasWatered = true;
                OnInteraction?.Invoke(new StationGiveItemInteraction() { giveItemEnum = _giveItemEnum }, playerCharacter.characterTypeEnum);
                OnInteraction?.Invoke(new StationChangeSpriteInteraction() { newSprite = _newSprite }, playerCharacter.characterTypeEnum);
            }
            else if(wasWatered && playerCharacter.collectedItem == null)
            {
                OnInteraction?.Invoke(new StationTakeItemInteraction() { takeItemEnum = _takeItemEnum }, playerCharacter.characterTypeEnum);
                OnInteraction?.Invoke(new StationChangeSpriteInteraction() { newSprite = _lastSprite }, playerCharacter.characterTypeEnum);

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