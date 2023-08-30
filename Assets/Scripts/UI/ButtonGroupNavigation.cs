using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupNavigation : MonoBehaviour
{
    private InputManager _inputManager;
    private List<Button> _buttons;
    private List<Outline> _buttonOutlines;
    private int _activeButtonIndex;
    private bool _isActive;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _inputManager.OnMoveActionPerformed += OnMoveActionPerformed;
        _inputManager.OnOkActionPerformed += OnOkActionPerformed;
    }

    public void Setup(Button[] buttons)
    {
        _activeButtonIndex = 0;
        _buttons = new List<Button>(buttons);
        _buttonOutlines = new List<Outline>();
        foreach (Button button in _buttons)
        {
            Outline outline = button.GetComponent<Outline>();
            outline.enabled = false;
            _buttonOutlines.Add(outline);
        }
        SetFocusedButton(0);
        _isActive = true;
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    public void OnButtonHover(Button button)
    {
        SetFocusedButton(button);
    }

    private void SetFocusedButton(Button button)
    {
        SetIndex(_buttons.IndexOf(button));
    }

    private void SetFocusedButton(int index)
    {
        if (index < 0f)
        {
            index = _buttons.Count - 1;
        }
        else if (index >= _buttons.Count)
        {
            index = 0;
        }
        SetIndex(index);
    }

    private void SetIndex(int index)
    {
        _buttonOutlines[_activeButtonIndex].enabled = false;
        _activeButtonIndex = index;
        _buttonOutlines[_activeButtonIndex].enabled = true;
    }

    private void OnMoveActionPerformed(object sender, Vector2 direction)
    {
        if (_isActive && direction.y != 0f)
        {
            SetFocusedButton(_activeButtonIndex + (direction.y > 0f ? -1 : 1));
        }
    }

    private void OnOkActionPerformed(object sender, EventArgs empty)
    {
        if (_isActive)
        {
            _buttons[_activeButtonIndex].onClick.Invoke();
        }
    }

    private void OnDestroy()
    {
        _inputManager.OnMoveActionPerformed -= OnMoveActionPerformed;
        _inputManager.OnOkActionPerformed -= OnOkActionPerformed;
    }
}
