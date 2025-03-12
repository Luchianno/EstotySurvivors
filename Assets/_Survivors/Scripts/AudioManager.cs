using DG.Tweening;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Inject] AppSettings appSettings;
    [Inject] SignalBus signalBus;


    void Start()
    {
        musicSource.volume = appSettings.MusicVolume;
        sfxSource.volume = appSettings.SFXVolume;
    }

    void OnEnable()
    {
        signalBus.Subscribe<PlaySfxSignal>(OnPlaySfx);
    }

    void OnDisable()
    {
        signalBus.Unsubscribe<PlaySfxSignal>(OnPlaySfx);
    }

    public void FadeMusicVolume(float volume, float duration = 1f, bool saveSettings = false)
    {
        musicSource.DOFade(volume, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (saveSettings)
                {
                    appSettings.MusicVolume = volume;
                    appSettings.SaveToPreferences();
                }
            });
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    void OnPlaySfx(PlaySfxSignal signal)
    {
        if (signal.IsPositional)
        {
            AudioSource.PlayClipAtPoint(signal.Clip, signal.Position, appSettings.SFXVolume);
        }
        else
        {
            sfxSource.PlayOneShot(signal.Clip, appSettings.SFXVolume);
        }
    }
}
