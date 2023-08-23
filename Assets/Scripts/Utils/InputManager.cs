using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput _playerInput;
    public event EventHandler OnOkActionPerformed;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.PlayerActionMap.OkAction.performed += OnOkAction;
    }

    public Vector2 GetPlayerMovement()
    {
        
        return _playerInput.PlayerActionMap.Move.ReadValue<Vector2>();
    }

    public void OnOkAction(InputAction.CallbackContext context)
    {
        OnOkActionPerformed?.Invoke(this, EventArgs.Empty);
    }
}
