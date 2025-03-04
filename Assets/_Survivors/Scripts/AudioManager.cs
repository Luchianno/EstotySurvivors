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
        sfxSource.PlayOneShot(signal.Clip);
    }
}
