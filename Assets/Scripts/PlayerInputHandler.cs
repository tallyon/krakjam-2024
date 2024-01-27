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
        private InputActionMap _actionMap;
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
            
            _actionMap = controls.FindActionMap("gameplay");
        
            _actionMap.FindAction("interaction").performed += OnInteractionPerformed;
            _actionMap.FindAction("ability1").performed += OnAbility1Performed;
            _actionMap.FindAction("ability2").performed += OnAbility2Performed;

            if (index > 0)
            {
                FindObjectOfType<PlayerInputManager>().splitScreen = true;
            }
        }

        private void OnDestroy()
        {
            _actionMap.FindAction("interaction").performed -= OnInteractionPerformed;
            _actionMap.FindAction("ability1").performed -= OnAbility1Performed;
            _actionMap.FindAction("ability2").performed -= OnAbility2Performed;
        }
        
        private void OnAbility2Performed(InputAction.CallbackContext obj)
        {
            Debug.Log("Ability 2!");
        }

        private void OnAbility1Performed(InputAction.CallbackContext obj)
        {
            Debug.Log("Ability 1!");
        }

        public void OnInteractionPerformed(InputAction.CallbackContext obj)
        {
            if (_currentInteractableObject != null)
            {
                _currentInteractableObject.Interact(_playerCharacter);
                Debug.Log("Interaction!");
            }
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