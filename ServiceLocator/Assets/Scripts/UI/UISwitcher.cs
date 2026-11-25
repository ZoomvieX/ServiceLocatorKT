using UnityEngine;

public class UISwitcher
{
    private IUIState _currentState;

    public void SwitchState(IUIState nextState)
    {
        if (_currentState == nextState || nextState == null)
            return;

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }
}