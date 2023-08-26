using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacleSpawner : MonoBehaviour
{
    [SerializeField] private RockObstacle _rockPrefab;

    private StageProgress _stageProgress;
    private float _upperBound;
    private float _lowerBound;
    private float _leftBound;
    private float _spawnPosition = 12f;
    private bool _isActive = false;
    private float _cooldownTimer = 0f;
    private float SpawnCooldown;

    private void Start()
    {
        SpawnCooldown = DifficultySettings.GetInstance().GetObstacleSpawnCooldown();
        Vector2 bounds = GameBounds.GetInstance().GetScreenBounds();
        _upperBound = bounds.y;
        _lowerBound = bounds.y * -1;
        _leftBound = (bounds.x * -1) - 2f;
        _isActive = true;
        _stageProgress = StageProgress.GetInstance();
        _stageProgress.OnStageClear += OnStageClear;
        _stageProgress.OnStageRestart += OnStageRestart;
        _stageProgress.OnStageDeath += OnStageDeath;
    }

    private void Update()
    {
        if (_isActive)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= SpawnCooldown)
            {
                RockObstacle rock = Instantiate(
                    _rockPrefab,
                    new Vector3(
                        _spawnPosition,
                        0f,
                        Mathf.RoundToInt(UnityEngine.Random.Range(_lowerBound, _upperBound))
                    ),
                    Quaternion.identity
                );
                rock.Setup(_leftBound);
                _cooldownTimer = 0f;
            }
        }    
    }

    private void OnStageClear(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _isActive = true;
    }

    private void OnStageDeath(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnDestroy()
    {
        _stageProgress.OnStageClear -= OnStageClear;
        _stageProgress.OnStageRestart -= OnStageRestart;
        _stageProgress.OnStageDeath -= OnStageDeath;
    }
}
