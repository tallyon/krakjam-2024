using System;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool isOneTimeUse;
    public int InteractionTimeMS;
    public Action<Interaction, CharacterTypeEnum> OnInteraction;
    public CharacterTypeEnum possiblePlayerInteraction;

    protected bool _wasUsed;

    public virtual void PlayInteraction(PlayerCharacter playerCharacter)
    {
    }

    public virtual List<Vector2> PlayAbility(PlayerCharacter playerCharacter)
    {
        return null;
    }
}
