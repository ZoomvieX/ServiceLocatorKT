using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainScreenView : MonoBehaviour
{
    [SerializeField] private Button _openButton;

    public event Action OnOpenClicked;

    private void Awake()
    {
        if (_openButton == null)
            _openButton = GetComponentInChildren<Button>(true);
    }

    public void SubscribeOpenButton(UnityAction action)
    {
        if (_openButton != null && action != null)
            _openButton.onClick.AddListener(action);
    }

    public void UnsubscribeOpenButton(UnityAction action)
    {
        if (_openButton != null && action != null)
            _openButton.onClick.RemoveListener(action);
    }

    public void SetOpenInteractable(bool isInteractable)
    {
        if (_openButton != null)
            _openButton.interactable = isInteractable;
    }

    public void BindInternalSignal()
    {
        SubscribeOpenButton(HandleOpenClick);
    }

    public void UnbindInternalSignal()
    {
        UnsubscribeOpenButton(HandleOpenClick);
    }

    private void HandleOpenClick()
    {
        OnOpenClicked?.Invoke();
    }
}