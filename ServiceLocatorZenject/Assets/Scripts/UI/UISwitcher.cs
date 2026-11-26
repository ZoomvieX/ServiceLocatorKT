using UnityEngine;
using Zenject;

public class UISwitcher
{
    private IUIState _currentState;

    [Inject]
    public UISwitcher()
    {
    }

    public void SwitchState(IUIState nextState)
    {
        if (_currentState == nextState || nextState == null)
            return;

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }
}