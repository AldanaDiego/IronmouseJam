using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput _playerInput;
    public event EventHandler OnOkActionPerformed;
    public event EventHandler<Vector2> OnMoveActionPerformed;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.PlayerActionMap.OkAction.performed += OnOkAction;
        _playerInput.PlayerActionMap.Move.performed += OnMoveAction;
    }

    public Vector2 GetPlayerMovement()
    {
        
        return _playerInput.PlayerActionMap.Move.ReadValue<Vector2>();
    }

    public void OnOkAction(InputAction.CallbackContext context)
    {
        OnOkActionPerformed?.Invoke(this, EventArgs.Empty);
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        OnMoveActionPerformed?.Invoke(this, context.ReadValue<Vector2>());
    }
}
