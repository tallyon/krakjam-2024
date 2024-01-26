using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CharacterSelectUIAnim : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    private RectTransform _rectTransform;
    private Vector3 startingPos;
    private int currentPosition;
    private InputActionMap _actionMap;
    private InputAction _movementAction;
    private int _inputvalue;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        startingPos = _rectTransform.position;
        _actionMap = controls.FindActionMap("character selection");
        _actionMap.FindAction("choose character").performed += MoveRect;
    }

    /// <summary>
    /// Move rect to left or right side of the screen
    /// </summary>
    /// <param name="direction">use negative or positive value to determine left or right direction</param>
    public void MoveRect(InputAction.CallbackContext obj)
    {
        Debug.Log($"read input");
        int direction = obj.ReadValue<int>();
        if (currentPosition == direction)
            return;
        currentPosition += direction;
        if (direction < 0)
        {
            _rectTransform.DOMoveX(startingPos.x - 50f, 0.5f);
        }
        else if (direction == 0)
        {
            _rectTransform.DOMoveX(startingPos.x, 0.5f);
        }
        else
        {
            _rectTransform.DOMoveX(startingPos.x + 50f, 0.5f);
        }
    }

}
