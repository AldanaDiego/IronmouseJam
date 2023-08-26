using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerTransform;
    private InputManager _inputManager;
    private StageProgress _stageProgress;
    private PlayerHealth _playerHealth;
    private SFXManager _sfxManager;
    private Vector3 _defaultPosition;
    private const float JUMP_TIME = 1.2f;
    private const float JUMP_HEIGHT = 3f;
    private const float JUMP_COOLDOWN = 0.2f;
    private bool _isJumping = false;
    private bool _isCooldown = false;
    private bool _canJump = true;
    private float _jumpTimer;
    private float _cooldownTimer;

    private void Start()
    {
        _sfxManager = SFXManager.GetInstance();
        _defaultPosition = _playerTransform.localPosition;
        _inputManager = InputManager.GetInstance();
        _inputManager.OnOkActionPerformed += OnOkActionPerformed;
        _stageProgress = StageProgress.GetInstance();
        _stageProgress.OnStageClear += OnStageClear;
        _stageProgress.OnStageRestart += OnStageRestart;
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.OnPlayerDeath += OnPlayerDeath;
    }

    private void Update()
    {
        if (_isJumping)
        {
            _jumpTimer += Time.deltaTime;
            if (_jumpTimer < JUMP_TIME)
            {
                _playerTransform.localPosition = new Vector3(
                    _playerTransform.localPosition.x,
                    Mathf.Sin(_jumpTimer * Mathf.PI / JUMP_TIME) * JUMP_HEIGHT,
                    _playerTransform.localPosition.z
                );
            }
            else
            {
                _playerTransform.localPosition = _defaultPosition;
                _animator.SetTrigger("Reset");
                _sfxManager.PlayJumpLanding();
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
        if (_canJump && !_isJumping && !_isCooldown)
        {
            _sfxManager.PlayJumpStart();
            _animator.SetTrigger("Jump");
            _jumpTimer = 0f;
            _isJumping = true;
        }
    }

    private void OnStageClear(object sender, EventArgs empty)
    {
        _canJump = false;
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _canJump = true;
    }

    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        _canJump = false;
    }

    private void OnDestroy()
    {
        _inputManager.OnOkActionPerformed -= OnOkActionPerformed;
        _stageProgress.OnStageClear -= OnStageClear;
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;
        _stageProgress.OnStageRestart -= OnStageRestart;
    }
}
