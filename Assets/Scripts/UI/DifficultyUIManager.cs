using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUIManager : MonoBehaviour
{
    private SceneTransitionManager _sceneManager;

    private void Start()
    {
        _sceneManager = SceneTransitionManager.GetInstance();    
    }

    public void OnDifficultySelected(int difficulty)
    {
        if (Enum.IsDefined(typeof(Consts.Difficulties), difficulty))
        {
            PlayerPrefs.SetInt("Difficulty", difficulty);
            _sceneManager.ChangeToGameScene();
        }
    }
}
