using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuButtonsClickHandler : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsButton;
    //[SerializeField] private Button _tutorialButton;
    [SerializeField] private GameObject _settingsWindow;

    public event Action OnStartButtonClick;
    public event Action OnSettingsClick;
    public event Action OnTutorialClick;

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartButtonClick);
        _settingsButton.onClick.AddListener(SettingsClick);
        //_tutorialButton.onClick.AddListener(TutorialClick);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartButtonClick);
        _settingsButton.onClick.RemoveListener(SettingsClick);
        //_tutorialButton.onClick.RemoveListener(TutorialClick);
    }

    private void StartButtonClick()
    {
        OnStartButtonClick?.Invoke();
    }

    private void SettingsClick()
    {
        _settingsWindow.SetActive(true);
        OnSettingsClick?.Invoke();
    }

    private void TutorialClick()
    {
        OnTutorialClick?.Invoke();
    }
}
