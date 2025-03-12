using UnityEngine;

public struct PlaySfxSignal
{
    public AudioClip Clip { get; }
    public bool IsPositional { get; }
    public Vector2 Position { get; }

    public PlaySfxSignal(AudioClip clip, bool isPositional = false, Vector2 position = default)
    {
        Clip = clip;
        IsPositional = isPositional;
        Position = position;
    }
}
