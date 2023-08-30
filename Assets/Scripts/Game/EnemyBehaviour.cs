using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyCollision _enemyCollision;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private Transform _boardTransform;
    [SerializeField] private Collider _collider;

    private const float SPAWN_OFFSET = 3f;
    private const float SPAWN_SPEED = 2.5f;
    private const float MOVEMENT_SPEED = 5f;
    private const float ELASTIC_FACTOR = (1f * ((float) Math.PI)) / 3f;

    private PauseManager _pauseManager;
    private Transform _player;
    private StageProgress _stageProgress;
    private Transform _transform;
    private Vector3 _enemyDefaultLocalPosition;
    private bool _previousIsActive;
    private bool _isActive = false;
    private float _spawnStartPosition;
    private float _activePosition;

    private void Start()
    {
        _collider.enabled = false;
        _pauseManager = PauseManager.GetInstance();
        _pauseManager.OnPauseStatusChanged += OnPauseStatusChanged;
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

        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (_isActive)
        {
            float positionDiff = _player.position.z - _transform.position.z;
            Vector3 direction = Vector3.zero;
            if (positionDiff >= 0.25f)
            {
                direction = Vector3.forward;
            }
            else if (positionDiff <= -0.25f)
            {
                direction = Vector3.back;
            }

            if (direction != Vector3.zero)
            {
                _transform.position += direction * (Time.deltaTime * MOVEMENT_SPEED);
            }
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }

    private void SetStartPosition()
    {
        _transform.position = new Vector3(_spawnStartPosition, 0f, 0f);
    }

    private IEnumerator EnterStage()
    {
        _collider.enabled = true;
        _enemyTransform.localPosition += Vector3.left * SPAWN_OFFSET;
        _boardTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        while (_transform.position.x < _activePosition)
        {
            _transform.position += Vector3.right * (Time.deltaTime * SPAWN_SPEED);
            float movementProgress = (_transform.position.x - _activePosition + 1.5f) / 1.5f;
            _boardTransform.localRotation = Quaternion.Euler(0f, 0f, 25f * ElasticEaseOut(movementProgress));
            yield return new WaitForFixedUpdate();
        }
        _transform.position = new Vector3(_activePosition, 0f, 0f);
        _boardTransform.localRotation = Quaternion.Euler(0f, 0f, 25f);

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

    private float ElasticEaseOut(float x)
    {
        if (x <= 0.01f)
        {
            return 0f;
        }
        if (x >= 0.99f)
        {
            return 1f;
        }
        return (float) (Math.Pow(2, -3 * x) * Math.Sin((x * 10 - 0.75f) * ELASTIC_FACTOR)) + 1;
    }

    private IEnumerator CollisionCooldown()
    {
        _isActive = false;
        yield return new WaitForSeconds(0.5f);
        _isActive = true;
    }

    private void OnEnemyCollision(object sender, EventArgs empty)
    {
        if (_isActive)
        {
            StartCoroutine(CollisionCooldown());
        }
        _animator.SetTrigger("Hit");
    }

    private void OnStageClear(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _isActive = false;
        _collider.enabled = false;
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

    private void OnPauseStatusChanged(object sender, bool isPaused)
    {
        if (isPaused)
        {
            _previousIsActive = _isActive;
            _isActive = false;
        }
        else
        {
            _isActive = _previousIsActive;
        }
    }

    private void OnDestroy()
    {
        _stageProgress.OnStageClear -= OnStageClear;
        _stageProgress.OnStageRestart -= OnStageRestart;
        _stageProgress.OnStageDeath -= OnStageDeath;
        _stageProgress.OnEnemySpawn -= OnEnemySpawn;
        _enemyCollision.OnEnemyCollision -= OnEnemyCollision;
    }
}
