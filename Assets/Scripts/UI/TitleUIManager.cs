using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _titleButtonMenu;
    [SerializeField] private GameObject _difficultyButtonMenu;

    private void Start()
    {
        _difficultyButtonMenu.SetActive(false);    
    }

    public void OnButtonPlayClicked()
    {
        _titleButtonMenu.SetActive(false);
        _difficultyButtonMenu.SetActive(true);
    }

    public void OnButtonSettingsClicked()
    {
        
    }

    public void OnButtonExitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnButtonBackClicked()
    {
        _difficultyButtonMenu.SetActive(false);
        _titleButtonMenu.SetActive(true);
    }
}
