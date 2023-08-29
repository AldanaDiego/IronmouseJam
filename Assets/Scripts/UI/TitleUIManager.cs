using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _titleButtonMenu;
    [SerializeField] private GameObject _difficultyButtonMenu;
    [SerializeField] private GameObject _settingsMenu;

    private SFXManager _sfxManager;

    private void Start()
    {
        _sfxManager = SFXManager.GetInstance();
        _difficultyButtonMenu.SetActive(false);
        _settingsMenu.SetActive(false);   
    }

    public void OnButtonPlayClicked()
    {
        _sfxManager.PlayButtonClicked();
        _titleButtonMenu.SetActive(false);
        _difficultyButtonMenu.SetActive(true);
    }

    public void OnButtonSettingsClicked()
    {
        _sfxManager.PlayButtonClicked();
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
    }
}
