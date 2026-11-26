using UnityEngine;
using Zenject;

public class SoundPlayer : ISoundPlayer
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip _openClip;
    private readonly AudioClip _closeClip;

    [Inject]
    public SoundPlayer(
        AudioSource audioSource,
        [Inject(Id = "OpenClip", Optional = true)] AudioClip openClip,
        [Inject(Id = "CloseClip", Optional = true)] AudioClip closeClip)
    {
        _audioSource = audioSource;
        _openClip = openClip;
        _closeClip = closeClip;
    }

    public void PlayOpenSound()
    {
        if (_audioSource == null) return;

        if (_openClip != null)
            _audioSource.PlayOneShot(_openClip);
        else
            _audioSource.Play();
    }

    public void PlayCloseSound()
    {
        if (_audioSource == null) return;

        if (_closeClip != null)
            _audioSource.PlayOneShot(_closeClip);
        else
            _audioSource.Play();
    }
}