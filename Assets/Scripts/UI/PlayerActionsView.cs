using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class PlayerActionsView : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Image playerIcon;
        [SerializeField] private Image interactableImage;
        [SerializeField] private Image interactableImageInputIcon;
        [SerializeField] private Image skill1;
        [SerializeField] private Image skill1InputIcon;
        [SerializeField] private Image skill2;
        [SerializeField] private Image skill2InputIcon;

        public void Setup(PlayerInput playerInput, PlayerCharacter playerCharacter)
        {
            content.SetActive(true);
            playerIcon.gameObject.SetActive(true);
            playerCharacter.onItemAdd += SetItemIcon;
            playerCharacter.onItemDeleted += ClearItem;
        }

        private void SetItemIcon(ItemsData.ItemsEnum item)
        {
            Debug.Log($"Setup UI for item added");
        }

        private void ClearItem()
        {
            Debug.Log($"Setup ui for item removed");
        }
        
    }
}