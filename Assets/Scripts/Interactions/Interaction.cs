using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float InteractionTime;
    public Action<Interaction, CharacterTypeEnum> OnInteraction;
    public CharacterTypeEnum possiblePlayerInteraction;

    public virtual bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        return true;
    }

    public virtual bool PlayAbility(PlayerCharacter playerCharacter)
    {
        return true;
    }
}
