using UnityEngine;

public class SoundPlayer : ISoundPlayer
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip _openClip;
    private readonly AudioClip _closeClip;

    public SoundPlayer(AudioSource audioSource, AudioClip openClip = null, AudioClip closeClip = null)
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