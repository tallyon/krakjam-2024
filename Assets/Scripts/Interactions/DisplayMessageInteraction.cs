public class DisplayMessageInteraction : Interaction
{
    public string Message;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
    }
}
