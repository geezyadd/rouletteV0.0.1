using System;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuSystem : IInitializable, IDisposable
{
    private GameStateButtonsHandler _gameStateButtonsHandler;

    public MenuSystem(GameStateButtonsHandler gameStateButtonsHandler)
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
    }


    private void GoToMainMenu()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
