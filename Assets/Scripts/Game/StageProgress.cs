using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : Singleton<StageProgress>
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private BackgroundTransition _backgroundTransition;

    public event EventHandler OnStageRestart;
    public event EventHandler OnStageClear;
    public event EventHandler OnStageDeath;

    private const float STAGE_RESTART_TIME = 3f;
    private float _stageTotalTime = 15f; //TODO depends on difficulty
    private float _stageTimer;
    private bool _isActive;

    private void Start()
    {
        _stageTimer = 0f;
        _isActive = true;
        _playerHealth.OnPlayerDeath += OnPlayerDeath;
    }

    private void Update()
    {
        if (_isActive)
        {
            _stageTimer += Time.deltaTime;
            if (_stageTimer >= _stageTotalTime)
            {
                _isActive = false;
                OnStageClear?.Invoke(this, EventArgs.Empty);
            }
        }    
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(_backgroundTransition.ExitScene());
        yield return new WaitForSeconds(0.25f);
        OnStageRestart?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(_backgroundTransition.EnterScene());
        _isActive = true;
    }

    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        OnStageDeath?.Invoke(this, EventArgs.Empty);
        _isActive = false;
        _stageTimer = 0f;
        StartCoroutine(RestartGame());
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;
    }
}
