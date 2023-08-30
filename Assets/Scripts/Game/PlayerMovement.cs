using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 8f;

    public event EventHandler OnPlayerMovedAway;

    private PauseManager _pauseManager;
    private InputManager _inputManager;
    private StageProgress _stageProgress;
    private PlayerHealth _playerHealth;
    private Transform _transform;
    private bool _previousCanMove;
    private bool _canMove = true;
    private float _movementBoundHorizontal;
    private float _movementBoundVertical;

    private void Start()
    {
        _transform = transform;
        _inputManager = InputManager.GetInstance();
        Vector2 bounds = GameBounds.GetInstance().GetScreenBounds();
        _movementBoundHorizontal = bounds.x * 0.55f;
        _movementBoundVertical = bounds.y;
        _stageProgress = StageProgress.GetInstance();
        _stageProgress.OnStageClear += OnStageClear;
        _stageProgress.OnStageRestart += OnStageRestart;
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.OnPlayerDeath += OnPlayerDeath;
        _pauseManager = PauseManager.GetInstance();
        _pauseManager.OnPauseStatusChanged += OnPauseStatusChanged;
    }

    private void Update()
    {
        if (_canMove)
        {
            Vector2 movement = _inputManager.GetPlayerMovement();
            if (movement != Vector2.zero)
            {
                movement = Vector2.ClampMagnitude(movement, 1f) * (MOVEMENT_SPEED * Time.deltaTime);
                _transform.position = new Vector3(
                    Mathf.Clamp(_transform.position.x + movement.x, _movementBoundHorizontal * -1, _movementBoundHorizontal),
                    0f,
                    Mathf.Clamp(_transform.position.z + movement.y, _movementBoundVertical * -1, _movementBoundVertical)
                );
            }
        }    
    }

    private IEnumerator MoveAway()
    {
        yield return new WaitForSeconds(2.5f);
        float leaveBound = (_movementBoundHorizontal / 0.55f) + 3f;
        while (_transform.position.x <= leaveBound)
        {
            _transform.position += Vector3.right * (MOVEMENT_SPEED * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        OnPlayerMovedAway?.Invoke(this, EventArgs.Empty);
    }

    private void OnStageClear(object sender, EventArgs empty)
    {
        _canMove = false;
        StartCoroutine(MoveAway());
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _transform.position = Vector3.zero; 
        _canMove = true;
    }
    
    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        _canMove = false;
    }

    private void OnPauseStatusChanged(object sender, bool isPaused)
    {
        if (isPaused)
        {
            _previousCanMove = _canMove;
            _canMove = false;
        }
        else
        {
            _canMove = _previousCanMove;
        }
    }

    private void OnDestroy()
    {
        _stageProgress.OnStageClear -= OnStageClear;
        _stageProgress.OnStageRestart -= OnStageRestart;
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;    
    }
}
