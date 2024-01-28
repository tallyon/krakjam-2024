
using System.Collections.Generic;
using UnityEngine;
using static SimpleTextPopAnimation;

public class DoorInteraction : Interaction
{
    [SerializeField] GameObject doorOpen;
    [SerializeField] GameObject doorClosed;
    [SerializeField] GameObject doorDestroyed;
    [SerializeField] Transform playerPosition;

    public DoorState doorState = DoorState.Open;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum);
            return;
        }

        if (doorState == DoorState.Open)
        {
            if(playerCharacter.characterTypeEnum == CharacterTypeEnum.Beta)
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NerdAbilityDoors), playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player cannot take this item as this character");
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player cannot take this item as this character");
            }
        }
        else if (doorState == DoorState.Close)
        {
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
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
    }

    public override List<Vector2> PlayAbility(PlayerCharacter playerCharacter)
    {
        if (isOneTimeUse && _wasUsed)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.Used), playerCharacter.characterTypeEnum);
            return null;
        }


        if (doorState == DoorState.Open && playerCharacter.characterTypeEnum == CharacterTypeEnum.Beta)
        {
            doorOpen.SetActive(false);
            doorClosed.SetActive(true);

            doorState = DoorState.Close;
            OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);

            return new List<Vector2>() { playerPosition.position };
        }
        if(doorState == DoorState.Close && playerCharacter.characterTypeEnum == CharacterTypeEnum.Sigma)
        {
            doorClosed.SetActive(false);
            doorDestroyed.SetActive(true);

            doorState = DoorState.Destroyed;
            OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            _wasUsed = true;
            return new List<Vector2>() { playerCharacter.transform.position };
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction(InfoEnums.NoInteraction), playerCharacter.characterTypeEnum);
            return null;
        }
    }

    public enum DoorState
    {
        Open,
        Close,
        Destroyed
    }
}