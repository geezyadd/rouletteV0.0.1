using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStateButtonsHandler : MonoBehaviour
{
    [SerializeField] private Button _spinButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _settingsWindow;

    public event Action OnSpinButtonClicked;
    public event Action OnSettingsClicked;
    public event Action OnMenuClicked;

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(SpinButton);
        _settingsButton.onClick.AddListener(SettingsButton);
        _menuButton.onClick.AddListener(MenuButton);
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveListener(SpinButton);
        _settingsButton.onClick.RemoveListener(SettingsButton);
        _menuButton.onClick.RemoveListener(MenuButton);
    }

    private void MenuButton()
    {
        OnMenuClicked?.Invoke();
    }

    private void SettingsButton()
    {
        _settingsWindow.active = true;
        OnSettingsClicked?.Invoke();
    }

    private void SpinButton()
    {
        OnSpinButtonClicked?.Invoke();
    }
}
