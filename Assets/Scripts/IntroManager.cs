using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _surfer1;
    [SerializeField] private ParticleSystem _smokeParticleSystem;
    [SerializeField] private Renderer _mouseFaceRenderer;
    [SerializeField] private Renderer _monkeFaceRenderer;
    [SerializeField] private List<Material> _angryFaceMaterial;
    [SerializeField] private List<Material> _surpriseFaceMaterial;
    [SerializeField] private List<Material> _scaredFaceMaterial;

    private Transform _cameraTransform;
    private SceneTransitionManager _sceneManager;
    private Dictionary<int, Action> _timedActions;
    private float _timer;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _sceneManager = SceneTransitionManager.GetInstance();
        _timedActions = new Dictionary<int, Action>();

        _timedActions.Add(0, () =>
        {
            SetPosition(_surfer1, new Vector3(-5.3f, 0.1f, -4.5f));

            SetPosition(_cameraTransform, new Vector3(8f, 1.1f, -4f));
            StartCoroutine(Move(_cameraTransform, new Vector3(0f, 1.2f, -6f), 1f));
        });

        _timedActions.Add(3, () =>
        {
            SetText(LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "INTRO_DIALOGUE_LOCATION"));
        });


        _timedActions.Add(7, () =>
        {
            SetText("");
            StartCoroutine(Move(_surfer1, new Vector3(-1f, 0.1f, -4f), 0.6f));
        });

        _timedActions.Add(13, () =>
        {
            _smokeParticleSystem.Play();
        });

        _timedActions.Add(14, () =>
        {
            SwapFace(_monkeFaceRenderer, _surpriseFaceMaterial);
        });

        _timedActions.Add(15, () =>
        {
            SwapFace(_mouseFaceRenderer, _scaredFaceMaterial);
        });
        
        _timedActions.Add(17, () =>
        {
            //jump, move table down
        });

        _timedActions.Add(18, () =>
        {
            Destroy(_surfer1.gameObject);
        });
        
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        int key = Mathf.FloorToInt(_timer);
        if (_timedActions.ContainsKey(key))
        {
            _timedActions[key].Invoke();
            _timedActions.Remove(key);
        }
    }

    private IEnumerator Move(Transform _transform, Vector3 position, float speed)
    {
        Vector3 direction = (position - _transform.position).normalized;
        while (Vector3.Distance(_transform.position, position) >= 0.05f)
        {
            _transform.position += direction * (Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        _transform.position = position;
    }

    private void SetPosition(Transform _transform, Vector3 position)
    {
        _transform.position = position;
    }

    private void SetText(string text)
    {
        _text.text = text;
    }

    private void SwapFace(Renderer face, List<Material> newMaterial)
    {
        face.SetMaterials(newMaterial);
    }
}
