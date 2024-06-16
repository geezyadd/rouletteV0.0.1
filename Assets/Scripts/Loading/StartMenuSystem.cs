using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartMenuSystem : IInitializable, IDisposable
{
    private StartMenuButtonsClickHandler _startMenuButtonsClickHandler;

    [Inject]

    public StartMenuSystem(StartMenuButtonsClickHandler startMenuButtonsClickHandler) {
        _startMenuButtonsClickHandler = startMenuButtonsClickHandler;
    }

    public void Initialize()
    {
        _startMenuButtonsClickHandler.OnStartButtonClick += StartGame;
        _startMenuButtonsClickHandler.OnSettingsClick += OpenSettings;
        _startMenuButtonsClickHandler.OnTutorialClick += StartTutorial;
    }

    public void Dispose()
    {
        _startMenuButtonsClickHandler.OnStartButtonClick -= StartGame;
        _startMenuButtonsClickHandler.OnSettingsClick -= OpenSettings;
        _startMenuButtonsClickHandler.OnTutorialClick -= StartTutorial;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OpenSettings()
    {
        Debug.LogError("Open Settings!!");
    }

    private void StartTutorial()
    {
        Debug.LogError("Start Tutorial!!");
    }

}
