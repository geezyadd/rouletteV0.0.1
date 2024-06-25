using System;
using System.Collections.Generic;

public class GameStateMachine {
    private Dictionary<Type, IState> _states;
    private IState _activeState;

    public GameStateMachine() {
        _states = new Dictionary<Type, IState>()
        {
            [typeof(CardsState)] = new CardsState(),
        };
    }

    public void Enter<TState>() where TState : IState
    {
        _activeState?.Exit();
        IState state = _states[typeof(TState)];
        _activeState = state;   
        state.Enter();
    }
    
}
