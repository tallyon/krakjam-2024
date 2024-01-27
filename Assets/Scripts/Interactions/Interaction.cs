using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Action<Interaction, CharacterTypeEnum> OnInteraction;
    public CharacterTypeEnum possiblePlayerInteraction;

    public virtual bool PlayInteraction(PlayerCharacter playerCharacter)
    {
        return true;
    }
}
