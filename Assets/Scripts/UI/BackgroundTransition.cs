using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup _backgroundImage;

    private SceneTransitionManager _sceneManager;
    private const float TRANSITION_TIME = 0.75f;

    private void Start()
    {
        _sceneManager = SceneTransitionManager.GetInstance();
        _sceneManager.OnSceneChanging += OnSceneChanging;

        StartCoroutine(EnterScene());
    }

    private IEnumerator EnterScene()
    {
        _backgroundImage.alpha = 1f;
        yield return new WaitForSeconds(0.1f);
        for (float i = 0; i < 1; i+= Time.deltaTime/TRANSITION_TIME)
        {
            _backgroundImage.alpha = Mathf.Lerp(1.75f, 0f, i);
            yield return new WaitForFixedUpdate();
        }
        _backgroundImage.gameObject.SetActive(false);
    }

    private IEnumerator ExitScene()
    {
        _backgroundImage.alpha = 0f;
        _backgroundImage.gameObject.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime / TRANSITION_TIME)
        {
            _backgroundImage.alpha = Mathf.Lerp(0f, 1.75f, i);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnSceneChanging(object sender, EventArgs empty)
    {
        StartCoroutine(ExitScene());
    }

    private void OnDestroy()
    {
        _sceneManager.OnSceneChanging -= OnSceneChanging;
    }
}
