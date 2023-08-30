using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private SFXManager _sfxManager;
    private PauseManager _pauseManager;

    private void Start()
    {
        _pauseManager = PauseManager.GetInstance();
        _sfxManager = SFXManager.GetInstance();
    }

    public void OnPauseButtonClicked()
    {
        _sfxManager.PlayButtonClicked();
        _pauseManager.SetPauseStatus(!_pauseManager.IsPaused());
    }
}
