using System;
using System.IO;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public enum SaverType
    {
        PlayerPrefs,
        Json
    }

    [Header("Views & Audio")]
    [SerializeField] private MainScreenView _mainScreenView;
    [SerializeField] private PopupPanelView _popupPanelView;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _closeClip;

    [Header("Gameplay & Save Settings")]
    [SerializeField] private int _scoreStep = 1;
    [SerializeField] private int _initialScore = 0;
    [SerializeField] private float _fadeDuration = 0.35f;
    [SerializeField] private SaverType _saverType = SaverType.PlayerPrefs;
    [SerializeField] private string _jsonFileName = "score_save.json";
    [SerializeField] private string _playerPrefsKey = "SavedScore";

    public override void InstallBindings()
    {
        // 1. Компоненты сцены и настройки
        Container.BindInstance(_mainScreenView).AsSingle();
        Container.BindInstance(_popupPanelView).AsSingle();
        Container.BindInstance(_audioSource).AsSingle();

        Container.Bind<AudioClip>().WithId("OpenClip").FromInstance(_openClip);
        Container.Bind<AudioClip>().WithId("CloseClip").FromInstance(_closeClip);
        Container.Bind<float>().WithId("FadeDuration").FromInstance(_fadeDuration);

        string saveTarget = _saverType == SaverType.Json
            ? Path.Combine(Application.persistentDataPath, _jsonFileName)
            : _playerPrefsKey;
        Container.Bind<string>().WithId("SavePath").FromInstance(saveTarget);

        // 2. Сервисы
        Container.Bind<IFadeService>().To<FadeService>().AsSingle();
        Container.Bind<ISoundPlayer>().To<SoundPlayer>().AsSingle();

        if (_saverType == SaverType.Json)
            Container.Bind<ISaver>().To<JsonSaver>().AsSingle();
        else
            Container.Bind<ISaver>().To<PlayerPrefsSaver>().AsSingle();

        // 3. Игровые объекты, навигация и состояния UI
        Container.Bind<Score>()
            .AsSingle()
            .WithArguments(_scoreStep, _initialScore);

        Container.Bind<UINavigator>().AsSingle();
        Container.Bind<UISwitcher>().AsSingle();
        Container.Bind<MainScreenState>().AsSingle();
        Container.Bind<PopupPanelState>().AsSingle();

        // 4. Неленивая инициализация стартового состояния сцены
        Container.BindInterfacesAndSelfTo<SceneStartController>().AsSingle().NonLazy();
    }

    private sealed class SceneStartController : IInitializable
    {
        private readonly UISwitcher _uiSwitcher;
        private readonly MainScreenState _mainScreenState;
        private readonly PopupPanelView _popupPanelView;

        [Inject]
        public SceneStartController(
            UISwitcher uiSwitcher,
            MainScreenState mainScreenState,
            PopupPanelView popupPanelView)
        {
            _uiSwitcher = uiSwitcher;
            _mainScreenState = mainScreenState;
            _popupPanelView = popupPanelView;
        }

        public void Initialize()
        {
            if (_popupPanelView != null && _popupPanelView.PanelImage != null)
            {
                var color = _popupPanelView.PanelImage.color;
                color.a = 0f;
                _popupPanelView.PanelImage.color = color;
                _popupPanelView.gameObject.SetActive(false);
            }

            _uiSwitcher.SwitchState(_mainScreenState);
        }
    }
}