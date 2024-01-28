using DG.Tweening;
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
    private Collider2D _collider;
    public Rigidbody2D Rigidbody => _rb2d;
    private Interactable _currentInteractableObject;
    private PlayerCharacter _playerCharacter;
    private Animator _animator;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _playerCharacter = GetComponent<PlayerCharacter>();
        _actionMap = controls.FindActionMap("gameplay");
        _movementAction = _actionMap.FindAction("movement");
        _animator = GetComponent<Animator>();
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
    
    public void OnChooseItem1Performed(InputAction.CallbackContext obj)
    {
        _playerCharacter.ApplyStatus(PlayerCharacterStatus.ChoosingItem);
        _playerCharacter.ChooseItem1();
        _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
    }

    public void OnChooseItem2Performed(InputAction.CallbackContext obj)
    {
        _playerCharacter.ApplyStatus(PlayerCharacterStatus.ChoosingItem);
        _playerCharacter.ChooseItem2();
        _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
    }

    public void OnChooseItem3Performed(InputAction.CallbackContext obj)
    {
        _playerCharacter.ApplyStatus(PlayerCharacterStatus.ChoosingItem);
        _playerCharacter.ChooseItem3();
        _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
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

    public void OnInteractionHold(InputAction.CallbackContext obj)
    {
        Debug.Log("INTERACTION HOLD");
    }

    public void OnUseItemPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Item used");
    }

    public void Move(Vector2 moveVal)
    {
        if (moveVal != Vector2.zero)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
               _animator.SetBool("IsMoving", false);
        }
        
        _rb2d.MovePosition(_rb2d.position + CharacterMoveSpeedModifier * moveVal);
    }

    public void EnterVent(float travelTime)
    {
        var positions = _currentInteractableObject.UseAbility(_playerCharacter);

        if (positions?.Count > 0)
        {
            Debug.Log("Entering the vent");
            _collider.enabled = false;
            _playerCharacter.ApplyStatus(PlayerCharacterStatus.InVent);

            var seq = DOTween.Sequence();

            float distance = Vector2.Distance(_playerCharacter.transform.position, positions[0]);
            for (int i =0; i<positions.Count -1; i++)
            {
                distance += Vector2.Distance(positions[i], positions[i + 1]);
            }

            float time = Vector2.Distance(_playerCharacter.transform.position, positions[0]) / distance * travelTime;
            seq.Append(_rb2d.DOMove(_playerCharacter.transform.position, time).SetEase(Ease.Linear));
            for (int i =0; i < positions.Count - 1; i++)
            {
                time = (Vector2.Distance(positions[i], positions[i+1]) / distance) * travelTime;
                seq.Append(_rb2d.DOMove(positions[i+1], time).SetEase(Ease.Linear));
            }

            seq.Play();
            seq.onComplete += () =>
            {
                Debug.Log("Exiting the vent");
                _collider.enabled = true;
                _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
            };
        }
    }
}
