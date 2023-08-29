using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUIManager : MonoBehaviour
{
    private SceneTransitionManager _sceneManager;
    private SFXManager _sfxManager;

    private void Start()
    {
        _sceneManager = SceneTransitionManager.GetInstance();
        _sfxManager = SFXManager.GetInstance();
    }

    public void OnDifficultySelected(int difficulty)
    {
        _sfxManager.PlayButtonClicked();
        if (Enum.IsDefined(typeof(Consts.Difficulties), difficulty))
        {
            PlayerPrefs.SetInt("Difficulty", difficulty);
            _sceneManager.ChangeToGameScene();
        }
    }
}
