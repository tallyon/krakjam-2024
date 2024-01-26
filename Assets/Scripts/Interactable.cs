    using Unity.VisualScripting;
    using UnityEngine;

    public class Interactable : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag(GameTags.PLAYER_1_TAG))
                Debug.Log($"interacted with {GameTags.PLAYER_1_TAG}");
            if(other.gameObject.CompareTag(GameTags.PLAYER_2_TAG))
                Debug.Log($"interacted with {GameTags.PLAYER_2_TAG}");
        }
    }
