using System.IO;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public enum SaverType
    {
        PlayerPrefs,
        Json
    }

    [Header("UI Views")]
    [SerializeField] private MainScreenView _mainScreenView;
    [SerializeField] private PopupPanelView _popupPanelView;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _closeClip;

    [Header("Animation & Save")]
    [SerializeField] private float _fadeDuration = 0.35f;
    [SerializeField] private SaverType _saverType = SaverType.PlayerPrefs;
    [SerializeField] private string _jsonFileName = "score_save.json";

    private ServiceLocator _serviceLocator;
    private UISwitcher _uiSwitcher;
    private MainScreenState _mainScreenState;
    private PopupPanelState _popupPanelState;
    private Score _score;

    private void Awake()
    {
        _serviceLocator = new ServiceLocator();

        IFadeService fadeService = new FadeService();
        ISoundPlayer soundPlayer = new SoundPlayer(_audioSource, _openClip, _closeClip);

        string jsonPath = Path.Combine(Application.persistentDataPath, _jsonFileName);
        ISaver saver = _saverType == SaverType.Json
            ? new JsonSaver()
            : new PlayerPrefsSaver();

        _serviceLocator.Register(fadeService);
        _serviceLocator.Register(soundPlayer);
        _serviceLocator.Register(saver);

        _score = new Score(step: 1, initialValue: 0);
        _uiSwitcher = new UISwitcher();

        string saveTarget = _saverType == SaverType.Json ? jsonPath : "PlayerPrefs_Score";

        _mainScreenState = new MainScreenState(
            _mainScreenView,
            () => _uiSwitcher.SwitchState(_popupPanelState)
        );

        _popupPanelState = new PopupPanelState(
            _popupPanelView,
            _serviceLocator.GetService<IFadeService>(),
            _serviceLocator.GetService<ISoundPlayer>(),
            _score,
            _serviceLocator.GetService<ISaver>(),
            saveTarget,
            _fadeDuration,
            () => _uiSwitcher.SwitchState(_mainScreenState)
        );

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