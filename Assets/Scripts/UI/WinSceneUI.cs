using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneUI : MonoBehaviour
{
    private SFXManager _sfxManager;
    private SceneTransitionManager _sceneManager;

    private void Start()
    {
        _sfxManager = SFXManager.GetInstance();
        _sceneManager = SceneTransitionManager.GetInstance();  
    }

    public void OnBackButtonClicked()
    {
        _sfxManager.PlayButtonClicked();
        _sceneManager.ChangeToTitleScene();
    }
}
