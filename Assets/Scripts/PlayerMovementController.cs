using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    [SerializeField] private float moveSpeed;

    private InputActionMap _actionMap;
    private InputAction _movementAction;
    private Vector2 _movementValue;
    private Rigidbody2D _rb2d;
    private Interactable _currentInteractableObject;
    private PlayerCharacter _playerCharacter;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerCharacter = GetComponent<PlayerCharacter>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameTags.INTERACTABLE))
        {
            var interactable = other.GetComponent<Interactable>();

            if(interactable != null)
            {
                _currentInteractableObject = interactable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            _currentInteractableObject = null;
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
        _rb2d.MovePosition(_rb2d.position + _movementValue * moveSpeed);
    }
}
