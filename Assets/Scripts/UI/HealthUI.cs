using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Transform _uiContainer;
    [SerializeField] private Image _heartImage;
    [SerializeField] private PlayerHealth _playerHealth;

    private List<Image> _hearts;

    private void Start()
    {
        _hearts = new List<Image>();
        _playerHealth.OnHealthChanged += OnHealthChanged;
        for (int i = 0; i < _playerHealth.GetMaxHealth(); i++)
        {
            Image image = Instantiate(_heartImage, _uiContainer);
            _hearts.Add(image);
        }
    }

    private void OnHealthChanged(object sender, int currentHealth)
    {
        int i = 0;
        while (i < currentHealth)
        {
            _hearts[i].color = Color.white;
            i++;
        }
        while (i < _playerHealth.GetMaxHealth())
        {
            _hearts[i].color = Color.black;
            i++;
        }
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthChanged += OnHealthChanged;
    }
}
