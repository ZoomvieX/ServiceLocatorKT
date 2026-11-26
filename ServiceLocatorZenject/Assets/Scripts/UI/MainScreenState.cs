using UnityEngine;
using System;
using Zenject;

public class MainScreenState : IUIState
{
    private readonly MainScreenView _mainView;
    private readonly UINavigator _navigator;

    [Inject]
    public MainScreenState(MainScreenView mainView, UINavigator navigator)
    {
        _mainView = mainView;
        _navigator = navigator;
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
        _navigator.OpenPopup();
    }
}