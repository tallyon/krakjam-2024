public class DisplayMessageInteraction : Interaction
{
    public string Message;

    public override bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);

        return false;
    }
}
