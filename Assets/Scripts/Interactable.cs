    using System;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using System.Linq;

    public class Interactable : MonoBehaviour
    {
        private ISimpleAnimation _simpleTextPopAnimation;
        private List<string> currentPlayerTags = new();

        private void Awake()
        {
            _simpleTextPopAnimation = GetComponentInChildren<ISimpleAnimation>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(GameTags.PLAYER_1_TAG) 
                || other.gameObject.CompareTag(GameTags.PLAYER_2_TAG))
            {
                if(currentPlayerTags.Any(val => other.gameObject.CompareTag(val)))
                    currentPlayerTags.Add(other.gameObject.tag);
                
                _simpleTextPopAnimation.PlayAnimation(transform.position);
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (currentPlayerTags.Any(val => other.gameObject.CompareTag(val)))
            {
                currentPlayerTags.Remove(other.gameObject.tag);
            }

            if (currentPlayerTags.Count <= 0)
            {
                _simpleTextPopAnimation.HideAnimation();
            }
        }
    }
