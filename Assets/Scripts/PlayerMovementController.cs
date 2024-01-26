using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    [SerializeField] private float moveSpeed;

    private InputAction _movementAction;
    private Vector2 _movementValue;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        var actionMap = controls.FindActionMap("gameplay");
        _movementAction = actionMap.FindAction("movement");
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
