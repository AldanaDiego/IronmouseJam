using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;

    public event EventHandler OnPlayerDeath;

    private StageProgress _stageProgress;
    private const int MAX_HEALTH = 3;
    private int _health;
    private bool _canBeDamaged;

    private void Start()
    {
        _health = MAX_HEALTH;
        _playerCollision.OnPlayerCollision += OnPlayerCollision;
        _stageProgress = StageProgress.GetInstance();
        _stageProgress.OnStageClear += OnStageClear;
        _stageProgress.OnStageRestart += OnStageRestart;
        _canBeDamaged = true;
    }

    private void OnPlayerCollision(object sender, EventArgs empty)
    {
        if (_canBeDamaged)
        {
            _health--;
            if (_health <= 0)
            {
                Debug.Log($"You died lul");
                _canBeDamaged = false;
                OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void OnStageClear(object sender, EventArgs empty)
    {
        _canBeDamaged = false;
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _health = MAX_HEALTH;
        _canBeDamaged = true;
    }

    private void OnDestroy()
    {
        _playerCollision.OnPlayerCollision -= OnPlayerCollision;
        _stageProgress.OnStageClear -= OnStageClear;
        _stageProgress.OnStageRestart -= OnStageRestart;
    }
}
