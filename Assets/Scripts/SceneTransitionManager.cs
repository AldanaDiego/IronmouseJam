using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    public event EventHandler OnSceneChanging;

    private const float SCENE_CHANGE_TIMER = 1f;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void ChangeToGameScene()
    {
        OnSceneChanging?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ChangeScene("GameScene"));
    }

    public void ChangeToTitleScene()
    {
        OnSceneChanging?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ChangeScene("TitleScene"));
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(SCENE_CHANGE_TIMER);
        SceneManager.LoadScene(sceneName);
    }
}
