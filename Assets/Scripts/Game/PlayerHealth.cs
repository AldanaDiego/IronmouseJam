using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerTransform;

    public event EventHandler OnPlayerDeath;
    public event EventHandler<int> OnHealthChanged;

    private StageProgress _stageProgress;
    private Vector3 _originalPosition;
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
        _originalPosition = _playerTransform.localPosition;
    }

    public int GetMaxHealth()
    {
        return MAX_HEALTH;
    }

    private void OnPlayerCollision(object sender, EventArgs empty)
    {
        if (_canBeDamaged)
        {
            _health--;
            OnHealthChanged?.Invoke(this, _health);
            if (_health <= 0)
            {
                StartCoroutine(DeathAnimation());
                _canBeDamaged = false;
                OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _animator.SetTrigger("Hit");
            }
        }
    }

    private IEnumerator DeathAnimation()
    {
        _animator.SetTrigger("Death");
        float animationTime = 0f;
        while (animationTime <= 1f)
        {
            animationTime += Time.deltaTime;
            _playerTransform.localPosition = new Vector3(
                0f - (animationTime * 1.5f),
                Mathf.Sin(animationTime * Mathf.PI) * 0.8f,
                0f
            );
            yield return new WaitForFixedUpdate();
        }
        animationTime = 0f;
        while (animationTime <= 0.35f)
        {
            animationTime += Time.deltaTime;
            _playerTransform.localPosition += Vector3.down * (Time.deltaTime * 1.2f);
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
        _playerTransform.localPosition = _originalPosition;
        _animator.SetTrigger("Reset");
        OnHealthChanged?.Invoke(this, _health);
    }

    private void OnDestroy()
    {
        _playerCollision.OnPlayerCollision -= OnPlayerCollision;
        _stageProgress.OnStageClear -= OnStageClear;
        _stageProgress.OnStageRestart -= OnStageRestart;
    }
}
