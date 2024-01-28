using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerMovementController : MonoBehaviour
{
    public const float STUNNING_SURFACE_STUN_DURATION_SECONDS = 3;
    
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
    [SerializeField] private Animator _animator;
    private StationOptionInteraction _optionInteractionInProgress;

    [SerializeField] public Vector2 MoveVelocity => currentFramePosition - lastFramePosition;
    private Vector2 lastFramePosition;
    private Vector2 currentFramePosition;
    [SerializeField] private Vector2 moveVelocity;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
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

        if (other.gameObject.CompareTag(GameTags.STUNNING_SURFACE))
        {
            _playerCharacter.ApplyStatus(PlayerCharacterStatus.Stunned, STUNNING_SURFACE_STUN_DURATION_SECONDS);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            _currentInteractableObject.OnSpecialInteractionPerformed -= HandleOnSpecialInteractionPerformed;
        }
        catch (Exception ex)
        {
            
        }

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
        Debug.Log("Player Clicked 1st item");
        if (_playerCharacter.PlayerStatus == PlayerCharacterStatus.ChoosingItem)
        {
            _playerCharacter.AddItem(_optionInteractionInProgress.takeItemEnum1);
            _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
            var stationChoiceObject = _currentInteractableObject as InteractableStations;
            stationChoiceObject.HideChoises();
            _optionInteractionInProgress = null;
        }
    }

    public void OnChooseItem2Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player Clicked 2nd item");
        if (_playerCharacter.PlayerStatus == PlayerCharacterStatus.ChoosingItem)
        {
            _playerCharacter.AddItem(_optionInteractionInProgress.takeItemEnum2);
            _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
            var stationChoiceObject = _currentInteractableObject as InteractableStations;
            stationChoiceObject.HideChoises();
            _optionInteractionInProgress = null;
        }
    }

    public void OnChooseItem3Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player Clicked 3rd item");
        if (_playerCharacter.PlayerStatus == PlayerCharacterStatus.ChoosingItem)
        {
            _playerCharacter.AddItem(_optionInteractionInProgress.takeItemEnum3);
            _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
            var stationChoiceObject = _currentInteractableObject as InteractableStations;
            stationChoiceObject.HideChoises();
            _optionInteractionInProgress = null;
        }
    }

    public void OnInteractionPerformed(InputAction.CallbackContext obj)
    {
        if (_currentInteractableObject != null)
        {
            _currentInteractableObject.OnSpecialInteractionPerformed += HandleOnSpecialInteractionPerformed;
            _currentInteractableObject.Interact(_playerCharacter);
            Debug.Log("Interaction!");
        }
        else
        {
            Debug.Log("No object to interact with");
        }
    }

    private void HandleOnSpecialInteractionPerformed(Interaction interaction)
    {
        if (interaction is StationOptionInteraction optionInteraction)
        { 
            _optionInteractionInProgress = optionInteraction;
            _playerCharacter.ApplyStatus(PlayerCharacterStatus.ChoosingItem);
            var stationChoiceObject = _currentInteractableObject as InteractableStations;

            stationChoiceObject.ShowChoices(optionInteraction.takeItemEnum1, optionInteraction.takeItemEnum2, optionInteraction.takeItemEnum3);
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
        _rb2d.MovePosition(_rb2d.position + CharacterMoveSpeedModifier * moveVal);
    }

    private void FixedUpdate()
    {
        lastFramePosition = currentFramePosition;
        currentFramePosition = _rb2d.position;

        moveVelocity = MoveVelocity;
        
        _animator.SetBool("IsMoving", MoveVelocity != Vector2.zero);
    }

    public bool CloseDoor()
    {
        if(_currentInteractableObject is InteractableStations station && station.StationEnum == StationsData.StationEnum.Door)
        {
            var positions = _currentInteractableObject.UseAbility(_playerCharacter);

            if (positions?.Count == 1)
            {
                Debug.Log("Closing doors");
            }

            return true;
        }

        return false;
    }

    public bool EnterVent(float travelTime)
    {
        if (_currentInteractableObject is InteractableStations station && station.StationEnum == StationsData.StationEnum.Vents)
        {
            var positions = _currentInteractableObject.UseAbility(_playerCharacter);

            if (positions?.Count > 0)
            {
                Debug.Log("Entering the vent");
                _collider.enabled = false;
                _playerCharacter.ApplyStatus(PlayerCharacterStatus.InVent);

                var seq = DOTween.Sequence();

                float distance = Vector2.Distance(_playerCharacter.transform.position, positions[0]);
                for (int i = 0; i < positions.Count - 1; i++)
                {
                    distance += Vector2.Distance(positions[i], positions[i + 1]);
                }

                float time = Vector2.Distance(_playerCharacter.transform.position, positions[0]) / distance * travelTime;
                seq.Append(_rb2d.DOMove(_playerCharacter.transform.position, time).SetEase(Ease.Linear));
                for (int i = 0; i < positions.Count - 1; i++)
                {
                    time = (Vector2.Distance(positions[i], positions[i + 1]) / distance) * travelTime;
                    seq.Append(_rb2d.DOMove(positions[i + 1], time).SetEase(Ease.Linear));
                }

                seq.Play();
                seq.onComplete += () =>
                {
                    Debug.Log("Exiting the vent");
                    _collider.enabled = true;
                    _playerCharacter.ApplyStatus(PlayerCharacterStatus.Normal);
                };
            }

            return true;
        }

        return false;
    }

    public void SmashItem()
    {
        if (_currentInteractableObject is InteractableStations station
            && (station.StationEnum == StationsData.StationEnum.TrophyCabinet
            || station.StationEnum == StationsData.StationEnum.Door))
        {
            var positions = _currentInteractableObject.UseAbility(_playerCharacter);

            if (positions?.Count > 0)
            {
                Debug.Log("Destroying the item");
            }
        }
    }
}
