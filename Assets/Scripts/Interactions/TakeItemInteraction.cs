public class TakeItemInteraction : Interaction
{
    public override bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(playerCharacter.characterTypeEnum  == CharacterTypeEnum.Both || playerCharacter.characterTypeEnum == possiblePlayerInteraction)
        {
            if (playerCharacter.collectedItem != null)
            {
                OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "You cannot have 2 collected items" }, playerCharacter.characterTypeEnum);
                return false;
            }
            else
            {
                OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
                return true;
            }
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Item cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            return false;
        }
    }
}
