using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private TMP_Dropdown _languageDropdown;
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        StartCoroutine(CreateLanguageDropdown()); 
    }

    public void OnEnable()
    {
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    private IEnumerator CreateLanguageDropdown()
    {
        yield return LocalizationSettings.InitializationOperation;

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        int currentLocaleIndex = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            Locale locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
            {
                currentLocaleIndex = i;
            }
            options.Add(new TMP_Dropdown.OptionData(locale.name));
        }
        
        _languageDropdown.options = options;
        _languageDropdown.value = currentLocaleIndex;
        _languageDropdown.onValueChanged.AddListener(OnLanguageLocaleChanged);
    }

    private void OnLanguageLocaleChanged(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    private void OnMusicVolumeSliderChanged(float value)
    {
        value = Mathf.Clamp(value, 0.001f, 1f);
        PlayerPrefs.SetFloat("MusicVolume", value);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    private void OnSFXVolumeSliderChanged(float value)
    {
        value = Mathf.Clamp(value, 0.001f, 1f);
        PlayerPrefs.SetFloat("SFXVolume", value);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
