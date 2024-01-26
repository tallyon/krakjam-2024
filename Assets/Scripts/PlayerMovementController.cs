using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    [SerializeField] private float moveSpeed;

    private InputActionMap _actionMap;
    private InputAction _movementAction;
    private Vector2 _movementValue;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _actionMap = controls.FindActionMap("gameplay");
        _movementAction = _actionMap.FindAction("movement");
        
        _actionMap.FindAction("interaction").performed += OnInteractionPerformed;
        _actionMap.FindAction("ability1").performed += OnAbility1Performed;
        _actionMap.FindAction("ability2").performed += OnAbility2Performed;
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
        Debug.Log("Interaction!");
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
