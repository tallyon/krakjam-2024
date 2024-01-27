using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionAsset controls;
        [SerializeField] private float moveSpeed = 0.1f;
        [SerializeField] private PlayerCamera playerCameraPrefab;

        private PlayerCharacter _playerCharacter;
        private PlayerInput _playerInput;
        private PlayerMovementController mover;
        private PlayerUIHandler uiSelector;
        private Vector2 _movementValue;
        private Interactable _currentInteractableObject;
        
        public void OnControlsChanged()
        {
            Debug.Log("Controls changed");
        }

        public void OnDeviceLost()
        {
            Debug.Log("Device lost");
        }

        public void OnDeviceRegained()
        {
            Debug.Log("Device regained");
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movementValue = context.ReadValue<Vector2>();
        }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            var index = _playerInput.playerIndex;
            var movers = FindObjectsOfType<PlayerMovementController>();
            mover = movers.FirstOrDefault(m => m.playerId == index);
            uiSelector = mover.GetComponent<PlayerUIHandler>();
            _playerCharacter = mover.GetComponent<PlayerCharacter>();
            _playerCharacter.OnPlayerCharacterStatusChanged += OnPlayerCharacterStatusChanged;
            var playerCamera = Instantiate(playerCameraPrefab);
            playerCamera.tag = index == 0 ? GameTags.PLAYER_1_TAG : GameTags.PLAYER_2_TAG;
            playerCamera.SetTarget(mover.transform);
            _playerInput.camera = playerCamera.Camera;
            
            GameStateController.Instance.Player1Score.OnPlayerWon += SwitchActionMapToEndGame;
            GameStateController.Instance.Player2Score.OnPlayerWon += SwitchActionMapToEndGame;
            GameStateController.Instance.OnGameStart += EnableInput;
            
            if (index > 0)
            {
                FindObjectOfType<PlayerInputManager>().splitScreen = true;
            }
        }

        private void OnPlayerCharacterStatusChanged(PlayerCharacterStatus status)
        {
            switch (status)
            {
                case PlayerCharacterStatus.Normal:
                    EnableInput();
                    break;
                case PlayerCharacterStatus.Stunned:
                    DisableInput();
                    break;
                case PlayerCharacterStatus.InVent:
                    DisableInput();
                    break;
                case PlayerCharacterStatus.ChoosingItem:
                    DisableInput();
                    break;
            }
        }

        public void OnAbility2Performed(InputAction.CallbackContext obj)
        {
            if (obj.started == false) return;
            mover.OnAbility2Performed(obj);
        }

        public void OnAbility1Performed(InputAction.CallbackContext obj)
        {
            if (obj.started == false) return;
            mover.OnAbility1Performed(obj);
        }

        public void OnChooseItem1Performed(InputAction.CallbackContext obj)
        {
            if (obj.canceled == false) return;
            mover.OnChooseItem1Performed(obj);
        }
        
        public void OnChooseItem2Performed(InputAction.CallbackContext obj)
        {
            if (obj.canceled == false) return;
            mover.OnChooseItem2Performed(obj);
        }

        public void OnChooseItem3Performed(InputAction.CallbackContext obj)
        {
            if (obj.canceled == false) return;
            mover.OnChooseItem3Performed(obj);
        }

        public void OnInteractionPerformed(InputAction.CallbackContext obj)
        {
            if (obj.started == false) return;
            mover.OnInteractionPerformed(obj);
        }

        public void OnInteractionHold(InputAction.CallbackContext obj)
        {
            if (obj.performed == false) return;
            mover.OnInteractionHold(obj);
        }

        public void OnUseItemPerformed(InputAction.CallbackContext obj)
        {
            if (obj.started == false) return;
            mover.OnUseItemPerformed(obj);
        }

        public void OnEndGameAbilityPressed(InputAction.CallbackContext obj)
        {
            Debug.Log($"Submit button pressed");
            GameStateController.Instance.ResetLevel();   
        }

        public void OnCharacterSelectionInput(InputAction.CallbackContext obj)
        {
            float dir = obj.ReadValue<float>();
            uiSelector.Move(dir);
        }

        private void SwitchActionMapToEndGame()
        {
            Debug.Log($"Remember to enable endgame action map before release");
            //_playerInput.SwitchCurrentActionMap("endgame");
        }

        private void EnableInput()
        {
            _playerInput.SwitchCurrentActionMap("gameplay");
        }

        private void DisableInput()
        {
            _playerInput.SwitchCurrentActionMap("idle");            
        }
        

        private void FixedUpdate()
        {
            if (mover == null) return;

            if (_movementValue != Vector2.zero)
            {
                mover.Move(_movementValue);
            }
        }
    }
}