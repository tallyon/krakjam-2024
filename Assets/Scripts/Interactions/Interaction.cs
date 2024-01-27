using System;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float InteractionTime;
    public Action<Interaction, CharacterTypeEnum> OnInteraction;
    public CharacterTypeEnum possiblePlayerInteraction;

    public virtual void PlayInteraction(PlayerCharacter playerCharacter)
    {
    }

    public virtual List<Vector2> PlayAbility(PlayerCharacter playerCharacter)
    {
        return null;
    }
}
