using UnityEngine;
using System;

public class MainScreenState : IUIState
{
    private readonly MainScreenView _mainView;
    private readonly Action _requestOpenPopup;

    public MainScreenState(MainScreenView mainView, Action requestOpenPopup)
    {
        _mainView = mainView;
        _requestOpenPopup = requestOpenPopup;
    }

    public void Enter()
    {
        _mainView.gameObject.SetActive(true);
        _mainView.SetOpenInteractable(true);
        _mainView.OnOpenClicked += HandleOpen;
        _mainView.BindInternalSignal();
    }

    public void Exit()
    {
        _mainView.SetOpenInteractable(false);
        _mainView.OnOpenClicked -= HandleOpen;
        _mainView.UnbindInternalSignal();
    }

    private void HandleOpen()
    {
        _requestOpenPopup?.Invoke();
    }
}