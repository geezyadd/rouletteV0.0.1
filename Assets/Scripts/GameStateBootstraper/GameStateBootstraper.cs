using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateBootstraper : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    private void InjectDependencies(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    void Start()
    {
        _gameStateMachine.Enter<SlotState>();
    }

    
}
