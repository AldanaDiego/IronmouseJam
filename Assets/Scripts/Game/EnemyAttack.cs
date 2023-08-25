using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bulletPrefab;

    private EnemyBehaviour _behaviour;
    private StageProgress _stageProgress;
    private Transform _transform;
    private Vector3 _bulletOffset = new Vector3(1f, 1.25f, 0f);
    private float _attackCooldown = 5f; //TODO depends on difficulty
    private float _attackTimer;

    private void Start()
    {
        _behaviour = GetComponent<EnemyBehaviour>();
        _stageProgress = StageProgress.GetInstance();
        _stageProgress.OnStageRestart += OnStageRestart;
        _attackTimer = 0f;
        _transform = transform;
    }

    private void Update()
    {
        if (_behaviour.IsActive())
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackCooldown)
            {
                _attackTimer = 0f;
                _animator.SetTrigger("Attack");
                Instantiate(_bulletPrefab, _transform.position + _bulletOffset, Quaternion.Euler(0f, 180f, 0f));
            }
        }    
    }

    private void OnStageRestart(object sender, EventArgs empty)
    {
        _attackTimer = 0f;
    }

    private void OnDestroy()
    {
        _stageProgress.OnStageRestart -= OnStageRestart;
    }
}
