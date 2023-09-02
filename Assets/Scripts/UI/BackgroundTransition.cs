using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup _backgroundImage;
    [SerializeField] private RectTransform _cutoffImage;
    [SerializeField] private bool _useCuttoffEnter = false;
    [SerializeField] private bool _useCuttoffExit = false;

    private SceneTransitionManager _sceneManager;
    private RectTransform _backgroundRectTransform;
    private const float TRANSITION_TIME = 0.75f;
    private const float MAX_CUTOFF_SIZE = 1500f;

    private void Start()
    {
        _backgroundRectTransform = _backgroundImage.GetComponent<RectTransform>();
        _sceneManager = SceneTransitionManager.GetInstance();
        _sceneManager.OnSceneChanging += OnSceneChanging;

        StartCoroutine(EnterScene());
    }

    public IEnumerator EnterScene()
    {
        _backgroundRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        SetCutoffSize(0f);
        _backgroundImage.alpha = 1f;

        if (_useCuttoffEnter)
        {
            for (float i = 0; i< 1; i += Time.deltaTime / TRANSITION_TIME)
            {
                SetCutoffSize(Mathf.Lerp(0f, MAX_CUTOFF_SIZE, i));
                yield return new WaitForFixedUpdate();
            }
            SetCutoffSize(MAX_CUTOFF_SIZE);
            _backgroundImage.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            for (float i = 0; i < 1; i += Time.deltaTime / TRANSITION_TIME)
            {
                _backgroundImage.alpha = Mathf.Lerp(1.75f, 0f, i);
                yield return new WaitForFixedUpdate();
            }
            _backgroundImage.gameObject.SetActive(false);
        }
    }

    public IEnumerator ExitScene()
    {
        _backgroundRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        if (_useCuttoffExit)
        {
            SetCutoffSize(MAX_CUTOFF_SIZE);
            _backgroundImage.alpha = 1f;
            _backgroundImage.gameObject.SetActive(true);
            for (float i = 0; i< 1; i += Time.deltaTime / TRANSITION_TIME)
            {
                SetCutoffSize(Mathf.Lerp(MAX_CUTOFF_SIZE, 0f, i));
                yield return new WaitForFixedUpdate();
            }
            SetCutoffSize(0f);
        }
        else
        {
            SetCutoffSize(0f);
            _backgroundImage.alpha = 0f;
            _backgroundImage.gameObject.SetActive(true);
            for (float i = 0; i< 1; i += Time.deltaTime / TRANSITION_TIME)
            {
                _backgroundImage.alpha = Mathf.Lerp(0f, 1.75f, i);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private void SetCutoffSize(float size)
    {
        _cutoffImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        _cutoffImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
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
