using UnityEngine;
using System;
using Zenject;

public class PopupPanelState : IUIState
{
    private readonly PopupPanelView _popupView;
    private readonly IFadeService _fadeService;
    private readonly ISoundPlayer _soundPlayer;
    private readonly Score _score;
    private readonly ISaver _saver;
    private readonly string _savePath;
    private readonly float _fadeDuration;
    private readonly UINavigator _navigator;

    [Inject]
    public PopupPanelState(
        PopupPanelView popupView,
        IFadeService fadeService,
        ISoundPlayer soundPlayer,
        Score score,
        ISaver saver,
        [Inject(Id = "SavePath")] string savePath,
        [Inject(Id = "FadeDuration")] float fadeDuration,
        UINavigator navigator)
    {
        _popupView = popupView;
        _fadeService = fadeService;
        _soundPlayer = soundPlayer;
        _score = score;
        _saver = saver;
        _savePath = savePath;
        _fadeDuration = fadeDuration;
        _navigator = navigator;
    }

    public void Enter()
    {
        _popupView.SetScore(_score.Value);
        _popupView.OnCloseClicked += HandleClose;
        _popupView.OnCollectClicked += HandleCollect;
        _score.OnChanged += _popupView.SetScore;
        _popupView.BindInternalSignals();

        _soundPlayer.PlayOpenSound();
        _fadeService.FadeIn(_popupView.PanelImage, _fadeDuration);
    }

    public void Exit()
    {
        _popupView.OnCloseClicked -= HandleClose;
        _popupView.OnCollectClicked -= HandleCollect;
        _score.OnChanged -= _popupView.SetScore;
        _popupView.UnbindInternalSignals();

        _soundPlayer.PlayCloseSound();
        _saver.SaveScore(_score.Value, _savePath);
        _fadeService.FadeOut(_popupView.PanelImage, _fadeDuration);
    }

    private void HandleCollect()
    {
        _score.Add();
    }

    private void HandleClose()
    {
        _navigator.ClosePopup();
    }
}