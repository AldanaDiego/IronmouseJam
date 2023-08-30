using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _titleButtonMenu;
    [SerializeField] private GameObject _difficultyButtonMenu;
    [SerializeField] private GameObject _settingsMenu;

    private SFXManager _sfxManager;
    private Button[] _titleButtons;
    private Button[] _difficultyButtons;
    private ButtonGroupNavigation _buttonGroupNavigation;

    private void Start()
    {
        _sfxManager = SFXManager.GetInstance();
        _difficultyButtonMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _titleButtons = _titleButtonMenu.GetComponentsInChildren<Button>();
        _difficultyButtons = _difficultyButtonMenu.GetComponentsInChildren<Button>();
        _buttonGroupNavigation = GetComponent<ButtonGroupNavigation>();
        _buttonGroupNavigation.Setup(_titleButtons);
    }

    public void OnButtonPlayClicked()
    {
        _sfxManager.PlayButtonClicked();
        _titleButtonMenu.SetActive(false);
        _difficultyButtonMenu.SetActive(true);
        _buttonGroupNavigation.Setup(_difficultyButtons);
    }

    public void OnButtonSettingsClicked()
    {
        _sfxManager.PlayButtonClicked();
        _buttonGroupNavigation.SetActive(false);
        _titleButtonMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void OnButtonExitClicked()
    {
        _sfxManager.PlayButtonClicked();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnButtonBackClicked()
    {
        _sfxManager.PlayButtonClicked();
        _difficultyButtonMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _titleButtonMenu.SetActive(true);
        _buttonGroupNavigation.Setup(_titleButtons);
    }
}
