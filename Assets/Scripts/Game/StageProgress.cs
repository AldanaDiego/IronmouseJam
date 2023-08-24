using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : Singleton<StageProgress>
{
    //public event EventHandler OnStageRestart;
    public event EventHandler OnStageClear;

    private const float STAGE_RESTART_TIME = 3f;
    private float _stageTotalTime = 15f; //TODO depends on difficulty
    private float _stageTimer;
    private bool _isActive;

    private void Start()
    {
        _stageTimer = 0f;
        _isActive = true;    
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

}
