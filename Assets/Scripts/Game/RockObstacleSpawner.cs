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
    private bool _stageHalfway;
    
    private float SpawnCooldown;
    private float MultipleObstacleChance;

    private void Start()
    {
        DifficultySettings difficultySettings = DifficultySettings.GetInstance();
        SpawnCooldown = difficultySettings.GetObstacleSpawnCooldown();
        MultipleObstacleChance = difficultySettings.GetMultipleObstacleChance();
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
                bool isMultiple = UnityEngine.Random.value < MultipleObstacleChance;
                int position = Mathf.RoundToInt(UnityEngine.Random.Range(_lowerBound, _upperBound));
                if (isMultiple)
                {
                    SpawnObstacle(position);
                    int offset = RandomChoice(2, Mathf.RoundToInt((_upperBound - _lowerBound) / 2) ) * RandomChoice(1, -1);
                    int secondPosition = position + offset;
                    if (secondPosition < _lowerBound || secondPosition > _upperBound)
                    {
                        secondPosition = position - offset;
                    }
                    SpawnObstacle(secondPosition);
                }
                else
                {
                    SpawnObstacle(position);
                }
                _cooldownTimer = 0f;
            }
        }    
    }

    private void SpawnObstacle(int position)
    {
        RockObstacle rock = Instantiate(
            _rockPrefab,
            new Vector3(
                _spawnPosition,
                0f,
                position
            ),
            Quaternion.identity
        );
        rock.Setup(_leftBound);
    }

    private int RandomChoice(int value1, int value2)
    {
        return UnityEngine.Random.value <= 0.5f ? value1 : value2;
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
