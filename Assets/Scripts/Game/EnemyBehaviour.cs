using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyCollision _enemyCollision;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _enemyTransform;

    private const float SPAWN_OFFSET = 3f;
    private const float SPAWN_SPEED = 3.5f;

    private StageProgress _stageProgress;
    private Transform _transform;
    private Vector3 _enemyDefaultLocalPosition;
    private bool _isActive = false;
    private float _spawnStartPosition;
    private float _activePosition;

    private void Start()
    {
        _stageProgress = StageProgress.GetInstance();
        _stageProgress.OnStageClear += OnStageClear;
        _stageProgress.OnStageRestart += OnStageRestart;
        _stageProgress.OnStageDeath += OnStageDeath;
        _stageProgress.OnEnemySpawn += OnEnemySpawn;
        _enemyCollision.OnEnemyCollision += OnEnemyCollision;

        _transform = transform;
        Vector2 bounds = GameBounds.GetInstance().GetScreenBounds();
        _spawnStartPosition = (bounds.x * -1) - SPAWN_OFFSET;
        _activePosition = (bounds.x * -1) + 1.5f;
        _enemyDefaultLocalPosition = new Vector3(0f, 0.3f, 0f);
        SetStartPosition();
    }

    private void Update()
    {
        
    }

    private void SetStartPosition()
    {
        _transform.position = new Vector3(_spawnStartPosition, 0f, 0f);
    }

    private IEnumerator EnterStage()
    {
        _enemyTransform.localPosition += Vector3.left * SPAWN_OFFSET;

        while (_transform.position.x < _activePosition)
        {
            _transform.position += Vector3.right * (Time.deltaTime * SPAWN_SPEED);
            yield return new WaitForFixedUpdate();
        }
        _transform.position = new Vector3(_activePosition, 0f, 0f);

        float jumpTimer = 0f;
        _animator.SetTrigger("Jump");
        while (jumpTimer < 1f)
        {
            _enemyTransform.localPosition = new Vector3(
                Mathf.Lerp(SPAWN_OFFSET * -1, 0f, jumpTimer * 1.2f),
                Mathf.Sin(jumpTimer * ((float) Math.PI)) * 2.5f,
                _enemyDefaultLocalPosition.z
            );
            jumpTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _enemyTransform.localPosition = _enemyDefaultLocalPosition;

        yield return new WaitForSeconds(0.5f);
        _isActive = true;
    }

    private void OnEnemyCollision(object sender, EventArgs empty)
    {
        _animator.SetTrigger("Hit");
    }

    private void OnStageClear(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _isActive = false;
        SetStartPosition();   
    }

    private void OnStageDeath(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnEnemySpawn(object sender, EventArgs empty)
    {
        StartCoroutine(EnterStage());
    }

    private void OnDestroy()
    {
        _enemyCollision.OnEnemyCollision -= OnEnemyCollision;
        _stageProgress.OnStageRestart += OnStageRestart;
    }
}
