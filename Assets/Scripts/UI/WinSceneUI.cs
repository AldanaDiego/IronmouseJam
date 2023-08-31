using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class WinSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _difficultyClearedText;

    private SFXManager _sfxManager;
    private SceneTransitionManager _sceneManager;
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _sfxManager = SFXManager.GetInstance();
        _sceneManager = SceneTransitionManager.GetInstance();
        _difficultyClearedText.text = DifficultySettings.GetInstance().GetDifficultyString() + ": " + LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "WIN_CLEARED");
        _inputManager.OnOkActionPerformed += OnOkActionPerformed;
    }

    public void OnBackButtonClicked()
    {
        _sfxManager.PlayButtonClicked();
        _sceneManager.ChangeToTitleScene();
    }

    private void OnOkActionPerformed(object sender, EventArgs empty)
    {
        OnBackButtonClicked();
    }

    private void OnDestroy()
    {
        _inputManager.OnOkActionPerformed -= OnOkActionPerformed;
    }
}
