using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerMovementController : MonoBehaviour
{
    public int playerId;
    [SerializeField] private InputActionAsset controls;
    public float CharacterMoveSpeedModifier { get; set; }
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

    public void OnAbility2Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Ability 2!");
        _playerCharacter.UseAbility2();
    }

    public void OnAbility1Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Ability 1!");
        _playerCharacter.UseAbility1();
    }

    public void OnInteractionPerformed(InputAction.CallbackContext obj)
    {
        if (_currentInteractableObject != null)
        {
            _currentInteractableObject.Interact(_playerCharacter);
            Debug.Log("Interaction!");
        }
        else
        {
            Debug.Log("No object to interact with");
        }
    }

    public void Move(Vector2 moveVal)
    {
        _rb2d.MovePosition(_rb2d.position + CharacterMoveSpeedModifier * moveVal);
    }
}
