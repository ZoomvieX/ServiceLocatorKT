using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPanelView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _collectButton;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _panelImage;

    public Image PanelImage => _panelImage;

    public event Action OnCloseClicked;
    public event Action OnCollectClicked;

    private void Awake()
    {
        if (_panelImage == null)
            _panelImage = GetComponent<Image>();
    }

    public void SubscribeCloseButton(UnityAction action)
    {
        if (_closeButton != null && action != null)
            _closeButton.onClick.AddListener(action);
    }

    public void UnsubscribeCloseButton(UnityAction action)
    {
        if (_closeButton != null && action != null)
            _closeButton.onClick.RemoveListener(action);
    }

    public void BindInternalSignals()
    {
        SubscribeCloseButton(HandleCloseClick);
        if (_collectButton != null)
            _collectButton.onClick.AddListener(HandleCollectClick);
    }

    public void UnbindInternalSignals()
    {
        UnsubscribeCloseButton(HandleCloseClick);
        if (_collectButton != null)
            _collectButton.onClick.RemoveListener(HandleCollectClick);
    }

    public void SetScore(int value)
    {
        if (_scoreText != null)
            _scoreText.text = value.ToString();
    }

    private void HandleCloseClick()
    {
        OnCloseClicked?.Invoke();
    }

    private void HandleCollectClick()
    {
        OnCollectClicked?.Invoke();
    }
}