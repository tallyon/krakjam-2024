
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interaction
{
    [SerializeField] GameObject doorOpen;
    [SerializeField] GameObject doorClosed;
    [SerializeField] GameObject doorDestroyed;
    [SerializeField] Transform playerPosition;

    public DoorState doorState = DoorState.Open;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (doorState == DoorState.Open)
        {
            if(playerCharacter.characterTypeEnum == CharacterTypeEnum.Beta)
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You have to use ability" }, playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player cannot take this item as this character");
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You cannot interact" }, playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player cannot take this item as this character");
            }
        }
        else if (doorState == DoorState.Close)
        {
            if (playerCharacter.characterTypeEnum == CharacterTypeEnum.Sigma)
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You have to use ability" }, playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player cannot take this item as this character");
            }
            else
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You cannot interact" }, playerCharacter.characterTypeEnum);
                Debug.Log("Interactions: Player cannot take this item as this character");
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Item cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
    }

    public override List<Vector2> PlayAbility(PlayerCharacter playerCharacter)
    {
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
            return new List<Vector2>() { playerCharacter.transform.position };
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be taken by this character" }, playerCharacter.characterTypeEnum);
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