using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    [SerializeField] private GameObject _bubi;
    [SerializeField] private Transform _surfer1;
    [SerializeField] private Transform _surfer2;
    [SerializeField] private Transform _mouseSurfer1;
    [SerializeField] private Transform _mouseFalling;
    [SerializeField] private ParticleSystem _smokeParticleSystem;
    [SerializeField] private Animator _mouseSurferAnimator;
    [SerializeField] private Animator _mouseFallingAnimator;
    
    [SerializeField] private Renderer _mouseFaceRenderer;
    [SerializeField] private Renderer _monkeFaceRenderer;
    [SerializeField] private List<Material> _defaultFaceMaterial;
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

        //This is an absolute mess, if you're reading this I'm sorry
        _timedActions.Add(0, () =>
        {
            SetPosition(_surfer1, new Vector3(-5.3f, 0.1f, -4.5f));
            SetPosition(_surfer2, new Vector3(-5.6f, 0.1f, -4.5f));
            SetPosition(_mouseFalling, new Vector3(-0.5f, 3f, -2f));
            _mouseFalling.gameObject.SetActive(false);

            SetPosition(_cameraTransform, new Vector3(8f, 1.1f, -4f));
            _cameraTransform.rotation = Quaternion.Euler(10f, -15f, 0f);
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
            _mouseSurferAnimator.SetTrigger("Jump");
            StartCoroutine(MoveLocal(_mouseSurfer1, new Vector3(0f, 3f, 0f), 3.5f));
            StartCoroutine(Move(_surfer1, new Vector3(-1f, -0.1f, -4f), 0.2f));
        });

        _timedActions.Add(18, () =>
        {
            Destroy(_surfer1.gameObject);
            _mouseFalling.gameObject.SetActive(true);
            _mouseFallingAnimator.SetTrigger("Fall");
        });

        _timedActions.Add(19, () =>
        {
            //Move mouse 2 down
            StartCoroutine(Move(_mouseFalling, new Vector3(-0.5f, 0.4f, -2f), 2.75f));
        });

        _timedActions.Add(20, () =>
        {
            SwapFace(_monkeFaceRenderer, _defaultFaceMaterial);
            _mouseFallingAnimator.SetTrigger("Idle");
        });

        _timedActions.Add(21, () =>
        {
            SetPosition(_cameraTransform, new Vector3(1f, 1.2f, -3.5f));
            Destroy(_bubi);
            Destroy(_mouseFalling.gameObject);
        });

        _timedActions.Add(24, () =>
        {
            SwapFace(_monkeFaceRenderer, _scaredFaceMaterial);
        });

        _timedActions.Add(25, () =>
        {
            SetText(LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "INTRO_DIALOGUE_WAIT"));
        });

        _timedActions.Add(27, () =>
        {
            SetText(LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "INTRO_DIALOGUE_WARNING"));
            SetPosition(_cameraTransform, new Vector3(-0.5f, 1.2f, -6f));
            StartCoroutine(Move(_cameraTransform, new Vector3(0.5f, 1.2f, -7f), 1f));
            StartCoroutine(Move(_surfer2, new Vector3(4f, 0.1f, -4f), 5f));
        });

        _timedActions.Add(29, () =>
        {
            SetPosition(_cameraTransform, new Vector3(-35f, -1f, -0.5f));
            _cameraTransform.rotation = Quaternion.Euler(-30f, -85f, 0f);
            SetPosition(_surfer2, new Vector3(-42f, 1.7f, 0f));
            _surfer2.rotation = Quaternion.Euler(0f, 0f, 10f);
            StartCoroutine(Move(_surfer2, new Vector3(-39f, 1.7f, 0f), 2f));
        });

        _timedActions.Add(30, () =>
        {
            SetText(LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "INTRO_DIALOGUE_BUBI"));
            StartCoroutine(Move(_surfer2, new Vector3(-35f, -1f, 0f), 2f));
        });

        _timedActions.Add(32, () =>
        {
            SetText("");
            _sceneManager.ChangeToGameScene();
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
        while (Vector3.Distance(_transform.position, position) >= 0.07f)
        {
            _transform.position += direction * (Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        _transform.position = position;
    }

    private IEnumerator MoveLocal(Transform _transform, Vector3 position, float speed)
    {
        Vector3 direction = (position - _transform.localPosition).normalized;
        while (Vector3.Distance(_transform.localPosition, position) >= 0.07f)
        {
            _transform.localPosition += direction * (Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        _transform.localPosition = position;
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
