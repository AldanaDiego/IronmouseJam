using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButtonList;

    private ButtonGroupNavigation _buttonGroupNavigation;
    private InputManager _inputManager;
    private SFXManager _sfxManager;
    private PauseManager _pauseManager;

    private void Start()
    {
        _pauseManager = PauseManager.GetInstance();
        _sfxManager = SFXManager.GetInstance();
        _inputManager = InputManager.GetInstance();
        _inputManager.OnCancelActionPerformed += OnCancelActionPerformed;
        _buttonGroupNavigation = GetComponent<ButtonGroupNavigation>();
        _buttonGroupNavigation.Setup(_pauseButtonList.GetComponentsInChildren<Button>());
        _buttonGroupNavigation.SetActive(false);
        _pauseMenu.SetActive(false);
    }

    public void OnPauseButtonClicked()
    {
        _sfxManager.PlayButtonClicked();
        bool current = _pauseManager.IsPaused();
        _pauseMenu.SetActive(!current);
        _buttonGroupNavigation.SetActive(!current);
        _pauseManager.SetPauseStatus(!current);
    }

    public void OnResumeButtonClicked()
    {
        OnPauseButtonClicked();
    }

    public void OnQuitButtonClicked()
    {
        OnPauseButtonClicked();
        SceneTransitionManager.GetInstance().ChangeToTitleScene();
    }

    private void OnCancelActionPerformed(object sender, EventArgs empty)
    {
        OnPauseButtonClicked();
    }

    private void OnDestroy()
    {
        _inputManager.OnCancelActionPerformed -= OnCancelActionPerformed;
    }
}
