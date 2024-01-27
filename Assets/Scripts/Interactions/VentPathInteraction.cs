using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VentPathInteraction : Interaction
{
    public List<Transform> rectTransforms;

    public override void PlayInteraction(PlayerCharacter playerCharacter)
    {
        if(playerCharacter.characterTypeEnum == CharacterTypeEnum.Beta)
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "you have to use ability" }, playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Item cannot be taken by this character" }, playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
        }
    }

    public override List<Vector2> PlayAbility(PlayerCharacter playerCharacter)
    {
        if (playerCharacter.characterTypeEnum == CharacterTypeEnum.Beta)
        {
            OnInteraction?.Invoke(this, CharacterTypeEnum.Beta);
            Debug.Log("Interactions: Player already have one item");

            return rectTransforms.Select(x => (Vector2) x.transform.position).ToList();
        }
        else
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Ability is on cooldown" }, playerCharacter.characterTypeEnum);
            Debug.Log("Interactions: Player cannot take this item as this character");
            return null;
        }
    }
}
