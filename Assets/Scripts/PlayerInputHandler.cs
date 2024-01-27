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

        private PlayerCharacter _playerCharacter;
        private PlayerInput _playerInput;
        private PlayerMovementController mover;
        private InputActionMap _actionMap;
        private InputAction _movementAction;
        private Vector2 _movementValue;
        private Interactable _currentInteractableObject;



        private void Awake()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerInput = GetComponent<PlayerInput>();
            var index = _playerInput.playerIndex;
            var movers = FindObjectsOfType<PlayerMovementController>();
            mover = movers.FirstOrDefault(m => m.playerId == index);
            
            _actionMap = controls.FindActionMap("gameplay");
            _movementAction = _actionMap.FindAction("movement");
        
            _actionMap.FindAction("interaction").performed += OnInteractionPerformed;
            _actionMap.FindAction("ability1").performed += OnAbility1Performed;
            _actionMap.FindAction("ability2").performed += OnAbility2Performed;
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

        private void OnInteractionPerformed(InputAction.CallbackContext obj)
        {
            if (_currentInteractableObject != null)
            {
                _currentInteractableObject.Interact(_playerCharacter);
                Debug.Log("Interaction!");
            }
        }

        private void Update()
        {
            _movementValue = _movementAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            if(mover != null)
                mover.Move(_movementValue);
        }
    }
}