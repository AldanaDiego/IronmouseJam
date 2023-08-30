using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    public event EventHandler<bool> OnPauseStatusChanged;

    private bool _status;

    private void Start()
    {
        _status = false;
    }

    public bool IsPaused()
    {
        return _status;
    }

    public void SetPauseStatus(bool isPaused)
    {
        if (_status != isPaused)
        {
            _status = isPaused;
            OnPauseStatusChanged?.Invoke(this, _status);
            Time.timeScale = _status ? 0 : 1;
        }
    }
}
