using static ItemsData;
using static SimpleTextPopAnimation;

public class DisplayMessageInteraction : Interaction
{
    public InfoEnums InfoEnum;
    public ItemsEnum ItemEnum;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        OnInteraction?.Invoke(this, playerCharacter.characterTypeEnum);
    }

    public DisplayMessageInteraction(InfoEnums infoEnum, ItemsEnum itemEnum = ItemsEnum.None)
    {
        InfoEnum = infoEnum;
        ItemEnum = itemEnum;
    }
}
