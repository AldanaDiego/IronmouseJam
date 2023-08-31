using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class StageProgress : Singleton<StageProgress>
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BackgroundTransition _backgroundTransition;

    public event EventHandler OnStageRestart;
    public event EventHandler OnStageClear;
    public event EventHandler OnStageHalfway;
    public event EventHandler OnStageDeath;
    public event EventHandler OnEnemySpawn;

    private const float STAGE_RESTART_TIME = 3f;
    private SFXManager _sfxManager;
    private float StageTotalTime;
    private float EnemySpawnTime;
    private float _stageTimer;
    private float _enemyTimer;
    private bool _hasEnemySpawned;
    private bool _isActive;
    private bool _hasReachedHalfway;

    private void Start()
    {
        DifficultySettings difficultySettings = DifficultySettings.GetInstance();
        StageTotalTime = difficultySettings.GetStageTotalTime();
        EnemySpawnTime = difficultySettings.GetEnemySpawnTime();
        _playerHealth.OnPlayerDeath += OnPlayerDeath;
        _playerMovement.OnPlayerMovedAway += OnPlayerMovedAway;

        _sfxManager = SFXManager.GetInstance();
        _stageTimer = 0f;
        _enemyTimer = 0f;
        _hasEnemySpawned = false;
        _hasReachedHalfway = false;
        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
        {
            _stageTimer += Time.deltaTime;
            _enemyTimer += Time.deltaTime;
            if (!_hasReachedHalfway && _stageTimer >= StageTotalTime / 2)
            {
                _hasReachedHalfway = true;
                OnStageHalfway?.Invoke(this, EventArgs.Empty);
            }
            if (_stageTimer >= StageTotalTime)
            {
                _isActive = false;
                _sfxManager.PlayVictory();
                OnStageClear?.Invoke(this, EventArgs.Empty);
            }
            if (!_hasEnemySpawned && _enemyTimer >= EnemySpawnTime)
            {
                _hasEnemySpawned = true;
                OnEnemySpawn?.Invoke(this, EventArgs.Empty);
            }
        }    
    }

    public float GetProgressPercentage()
    {
        return _stageTimer / StageTotalTime;
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
        _hasReachedHalfway = false;
        StartCoroutine(RestartGame());
    }

    private void OnPlayerMovedAway(object sender, EventArgs empty)
    {
        SceneTransitionManager.GetInstance().ChangeToWinScene();
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;
    }
}
