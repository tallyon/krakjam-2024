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
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerInput = GetComponent<PlayerInput>();
            var index = _playerInput.playerIndex;
            var movers = FindObjectsOfType<PlayerMovementController>();
            mover = movers.FirstOrDefault(m => m.playerId == index);

            var playerCamera = Instantiate(playerCameraPrefab);
            playerCamera.tag = index == 0 ? GameTags.PLAYER_1_TAG : GameTags.PLAYER_2_TAG;
            playerCamera.SetTarget(mover.transform);
            _playerInput.camera = playerCamera.Camera;

            if (index > 0)
            {
                FindObjectOfType<PlayerInputManager>().splitScreen = true;
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

        public void OnInteractionPerformed(InputAction.CallbackContext obj)
        {
            if (obj.started == false) return;
            mover.OnInteractionPerformed(obj);
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