using UnityEngine;
using Zenject;

public class UINavigator
{
    private readonly DiContainer _container;
    private readonly UISwitcher _uiSwitcher;

    [Inject]
    public UINavigator(DiContainer container, UISwitcher uiSwitcher)
    {
        _container = container;
        _uiSwitcher = uiSwitcher;
    }

    public void OpenPopup()
    {
        _uiSwitcher.SwitchState(_container.Resolve<PopupPanelState>());
    }

    public void ClosePopup()
    {
        _uiSwitcher.SwitchState(_container.Resolve<MainScreenState>());
    }
}