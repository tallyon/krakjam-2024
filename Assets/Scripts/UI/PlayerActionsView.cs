using TMPro;
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
        [SerializeField] private AbilityTimer skill1Timer;
        [SerializeField] private Image skill1InputIcon;
        [SerializeField] private Image skill2;
        [SerializeField] private Image skill2InputIcon;
        [SerializeField] private AbilityTimer skill2Timer;
        [SerializeField] private InputIconsConfig iconsConfig;

        public void Setup(PlayerInput playerInput, PlayerCharacter playerCharacter)
        {
            content.SetActive(true);
            playerIcon.gameObject.SetActive(true);
            playerCharacter.onItemAdd += SetItemIcon;
            playerCharacter.onItemDeleted += ClearItem;
            playerCharacter.onAbility1Used += OnAbility1Used;
            playerCharacter.onAbility2Used += OnAbility2Used;
            var iconSet = iconsConfig.GetIcons(playerInput.devices[0].GetType().Name);
            skill1.sprite = playerCharacter.CharacterData.Ability1Sprite;
            skill2.sprite = playerCharacter.CharacterData.Ability2Sprite;
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

        private void OnAbility1Used(Ability ability)
        {
            Debug.Log($"On ability used {ability.CooldownLeftSeconds}");
            skill1Timer.StartCooldown(ability.CooldownLeftSeconds);
        }
        
        private void OnAbility2Used(Ability ability)
        {
            skill2Timer.StartCooldown(ability.CooldownLeftSeconds);
        }
    }
}