using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    [SerializeField] private float moveSpeed;

    private InputAction _movementAction;
    private Vector2 _movementValue;

    private void Awake()
    {
        var actionMap = controls.FindActionMap("gameplay");
        _movementAction = actionMap.FindAction("movement");
        _movementAction.performed += MovementActionOnperformed;
    }

    private void MovementActionOnperformed(InputAction.CallbackContext obj)
    {
        _movementValue = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // move according to input controls
        transform.Translate(_movementValue * moveSpeed);
    }
}
