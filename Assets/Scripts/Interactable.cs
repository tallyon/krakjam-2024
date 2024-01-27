using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactable : MonoBehaviour
{
    [SerializeField] List<Interaction> interactions;
    private ISimpleAnimation _simpleTextPopAnimation;
    private List<string> currentPlayerTags = new();
    private int _interactionIndex = 0;
    private Interaction _activeInteraction;


    private void Awake()
    {
        _simpleTextPopAnimation = GetComponentInChildren<ISimpleAnimation>();
        if(interactions.Count > 0)
        {
            _activeInteraction = interactions[0];
            _activeInteraction.OnInteraction += HandleOnInteractionWithObject;
        }
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
        if (_interactionIndex < interactions.Count)
        {
            if(_activeInteraction == null)
            {
                _activeInteraction = interactions[_interactionIndex];
            }

            var isInteractionComplete = _activeInteraction.PlayInteraction(playerCharacter);

            if (isInteractionComplete)
            {
                _activeInteraction.OnInteraction -= HandleOnInteractionWithObject;
                if (_interactionIndex + 1 < interactions.Count)
                {
                    _interactionIndex++;
                    _activeInteraction = interactions[_interactionIndex];
                    _activeInteraction.OnInteraction += HandleOnInteractionWithObject;
                }
            }
        }
    }

    protected virtual void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        switch (interaction)
        {
            case DisplayMessageInteraction displayMessageInteraction:
                _simpleTextPopAnimation.PlayAnimation(transform.position);
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
    //        _simpleTextPopAnimation.HideAnimation();
    //    }
    //}
}
