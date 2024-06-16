using System;
using UnityEngine.SceneManagement;
using Zenject;

public class SettingsAndMenuSystem : IInitializable, IDisposable
{
    private GameStateButtonsHandler _gameStateButtonsHandler;

    public SettingsAndMenuSystem(GameStateButtonsHandler gameStateButtonsHandler)
    {
        _gameStateButtonsHandler = gameStateButtonsHandler;
    }
    public void Dispose()
    {
        _gameStateButtonsHandler.OnMenuClicked -= GoToMainMenu;
    }

    public void Initialize()
    {
        _gameStateButtonsHandler.OnMenuClicked += GoToMainMenu;
        _gameStateButtonsHandler.OnSettingsClicked += OpenSettings;
    }

    private void OpenSettings()
    {
        throw new NotImplementedException();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
