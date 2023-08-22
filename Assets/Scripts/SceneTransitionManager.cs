using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    public event EventHandler OnSceneChanging;

    private const float SCENE_CHANGE_TIMER = 1f;
    private bool _shouldSceneFadeIn = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public bool ShouldSceneFadeIn()
    {
        return _shouldSceneFadeIn;
    }

    public void ChangeToGameScene()
    {
        _shouldSceneFadeIn = true;
        OnSceneChanging?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ChangeScene("GameScene"));
    }

    public void ChangeToTitleScene()
    {
        _shouldSceneFadeIn = true;
        OnSceneChanging?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ChangeScene("TitleScene"));
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(SCENE_CHANGE_TIMER);
        SceneManager.LoadScene(sceneName);
    }
}
