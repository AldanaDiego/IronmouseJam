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

    private void Start()
    {
        _sfxManager = SFXManager.GetInstance();
        _sceneManager = SceneTransitionManager.GetInstance();
        _difficultyClearedText.text = DifficultySettings.GetInstance().GetDifficultyString() + ": " + LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "WIN_CLEARED");
    }

    public void OnBackButtonClicked()
    {
        _sfxManager.PlayButtonClicked();
        _sceneManager.ChangeToTitleScene();
    }
}
