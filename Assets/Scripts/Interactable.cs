using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Interactable : MonoBehaviour
{
    public Action<Interaction> OnInteractionWithObject;

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

    public void Interact(string playerTag)
    {
        if (_interactionIndex < interactions.Count)
        {
            var isInteractionComplete = _activeInteraction.PlayInteraction(playerTag);

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

    private void HandleOnInteractionWithObject(Interaction interaction)
    {
        switch (interaction)
        {
            case DisplayMessageInteraction displayMessageInteraction:
                _simpleTextPopAnimation.PlayAnimation(transform.position);
                Debug.Log(displayMessageInteraction.Message);
                break;
            case TakeItemInteraction:
                OnInteractionWithObject?.Invoke(interaction);
                Destroy(gameObject);
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
