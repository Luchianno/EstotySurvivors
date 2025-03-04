using UnityEngine;

public struct PlaySfxSignal
{
    public AudioClip Clip { get; }

    public PlaySfxSignal(AudioClip clip)
    {
        Clip = clip;
    }
}
