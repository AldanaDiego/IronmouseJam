using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private InputManager _inputManager;
    private Transform _transform;
    private Vector3 _defaultPosition;
    private const float JUMP_TIME = 1.2f;
    private const float JUMP_HEIGHT = 3f;
    private const float JUMP_COOLDOWN = 0.2f;
    private bool _isJumping = false;
    private bool _isCooldown = false;
    private float _jumpTimer;
    private float _cooldownTimer;

    private void Start()
    {
        _transform = transform;
        _defaultPosition = _transform.localPosition;
        _inputManager = InputManager.GetInstance();
        _inputManager.OnOkActionPerformed += OnOkActionPerformed;
    }

    private void Update()
    {
        if (_isJumping)
        {
            _jumpTimer += Time.deltaTime;
            if (_jumpTimer < JUMP_TIME)
            {
                _transform.localPosition = new Vector3(
                    _transform.localPosition.x,
                    Mathf.Sin(_jumpTimer * Mathf.PI / JUMP_TIME) * JUMP_HEIGHT,
                    _transform.localPosition.z
                );
            }
            else
            {
                _transform.localPosition = _defaultPosition;
                _animator.SetTrigger("Reset");
                _isCooldown = true;
                _cooldownTimer = 0f;
                _isJumping = false;
            }
        }
        else if (_isCooldown)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= JUMP_COOLDOWN)
            {
                _isCooldown = false;
            }
        } 
    }

    private void OnOkActionPerformed(object sender, EventArgs empty)
    {
        if (!_isJumping && !_isCooldown)
        {
            _animator.SetTrigger("Jump");
            _jumpTimer = 0f;
            _isJumping = true;
        }
    }
}
