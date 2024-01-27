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
        [SerializeField] private InputIconsConfig iconsConfig;

        public void Setup(PlayerInput playerInput, PlayerCharacter playerCharacter)
        {
            content.SetActive(true);
            playerIcon.gameObject.SetActive(true);
            playerCharacter.onItemAdd += SetItemIcon;
            playerCharacter.onItemDeleted += ClearItem;
            var iconSet = iconsConfig.GetIcons(playerInput.devices[0].GetType().Name);
            skill1InputIcon.sprite = iconSet.icons.skill1Icon;
            skill2InputIcon.sprite = iconSet.icons.skill2Icon;
            interactableImageInputIcon.sprite = iconSet.icons.interactableIcon;
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