using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Interactable : MonoBehaviour
{
    public Action<Interaction> OnSpecialInteractionPerformed;

    [SerializeField] protected SimpleTextPopAnimation _simpleTextPopAnimationMiddle;
    [SerializeField] protected ParticleSystem particle;
    private Interaction interaction;
    private List<string> currentPlayerTags = new();


    protected virtual void Start()
    {
        interaction = this.GetComponent<Interaction>();
        interaction.OnInteraction += HandleOnInteractionWithObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameTags.PLAYER_1_TAG) 
            || other.gameObject.CompareTag(GameTags.PLAYER_2_TAG))
        {

            if (currentPlayerTags.Any(val => other.gameObject.CompareTag(val)))
                currentPlayerTags.Add(other.gameObject.tag);
        }

    }

    public void Interact(PlayerCharacter playerCharacter)
    {
        interaction.PlayInteraction(playerCharacter);
    }

    public List<Vector2> UseAbility(PlayerCharacter playerCharacter)
    {
        return interaction.PlayAbility(playerCharacter);
    }

    protected virtual void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        switch (interaction)
        {
            case DisplayMessageInteraction displayMessageInteraction:
                _simpleTextPopAnimationMiddle.PlayAnimation(transform.position);
                Debug.Log(displayMessageInteraction.Message);
                break;
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (currentPlayerTags.Any(val => other.gameObject.CompareTag(val)))
    //    {
    //        currentPlayerTags.Remove(other.gameObject.tag);
    //    }

    //    if (currentPlayerTags.Count <= 0)
    //    {
    //        _simpleTextPopAnimation1.HideAnimation();
    //    }
    //}
}
