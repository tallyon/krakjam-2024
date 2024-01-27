
using UnityEngine;

public class DoorInteraction : Interaction
{
    [SerializeField] GameObject doorOpen;
    [SerializeField] GameObject doorClosed;
    [SerializeField] GameObject doorDestroyed;

    public DoorState doorState = DoorState.Open;

    public override bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        if (doorState == DoorState.Open && playerCharacter.characterTypeEnum == CharacterTypeEnum.Beta)
        {
            doorOpen.SetActive(false);
            doorClosed.SetActive(true);

            doorState = DoorState.Close;
            OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            return false;
        }
        if(doorState == DoorState.Close && playerCharacter.characterTypeEnum == CharacterTypeEnum.Sigma)
        {
            doorClosed.SetActive(false);
            doorDestroyed.SetActive(true);

            doorState = DoorState.Destroyed;
            OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
            return true;
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Station cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            return false;
        }
    }

    public enum DoorState
    {
        Open,
        Close,
        Destroyed
    }
}