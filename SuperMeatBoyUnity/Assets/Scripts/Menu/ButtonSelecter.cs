using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelecter : MonoBehaviour
{
    protected int _currentButtonIndex = 0;
    [SerializeField] protected Button[] _buttons;

    private void Start() => SelectButton(_currentButtonIndex);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            SelectPreviousButton();

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            SelectNextButton();

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            PressSelectedButton();
    }

    protected void SelectPreviousButton()
    {
        _currentButtonIndex--;
        if (_currentButtonIndex < 0)
            _currentButtonIndex = _buttons.Length - 1;

        SelectButton(_currentButtonIndex);
    }

    protected void SelectNextButton()
    {
        _currentButtonIndex++;
        if (_currentButtonIndex >= _buttons.Length)
            _currentButtonIndex = 0;

        SelectButton(_currentButtonIndex);
    }

    protected void SelectButton(int index) => _buttons[_currentButtonIndex].Select();

    protected virtual void PressSelectedButton() => _buttons[_currentButtonIndex].onClick.Invoke();

}
