using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput _playerInput;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerInput.PlayerActionMap.Move.ReadValue<Vector2>();
    }
}
