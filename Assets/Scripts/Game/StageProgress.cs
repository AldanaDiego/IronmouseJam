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
    public event EventHandler OnEnemySpawn;

    private const float STAGE_RESTART_TIME = 3f;
    private float _stageTotalTime = 35f; //TODO depends on difficulty
    private float _enemySpawnTime = 10f; //TODO depends on difficulty 
    private float _stageTimer;
    private float _enemyTimer;
    private bool _hasEnemySpawned;
    private bool _isActive;

    private void Start()
    {
        _stageTimer = 0f;
        _enemyTimer = 0f;
        _hasEnemySpawned = false;
        _isActive = true;
        _playerHealth.OnPlayerDeath += OnPlayerDeath;
    }

    private void Update()
    {
        if (_isActive)
        {
            _stageTimer += Time.deltaTime;
            _enemyTimer += Time.deltaTime;
            if (_stageTimer >= _stageTotalTime)
            {
                _isActive = false;
                OnStageClear?.Invoke(this, EventArgs.Empty);
            }
            if (!_hasEnemySpawned && _enemyTimer >= _enemySpawnTime)
            {
                _hasEnemySpawned = true;
                OnEnemySpawn?.Invoke(this, EventArgs.Empty);
            }
        }    
    }

    public float GetProgressPercentage()
    {
        return _stageTimer / _stageTotalTime;
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
        _enemyTimer = 0f;
        _hasEnemySpawned = false;
        StartCoroutine(RestartGame());
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;
    }
}
