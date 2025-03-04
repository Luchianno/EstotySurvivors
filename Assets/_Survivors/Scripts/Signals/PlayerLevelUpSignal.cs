using UnityEngine;

public struct PlayerLevelUpSignal 
{
    public int Level { get; }

    public PlayerLevelUpSignal(int level)
    {
        Level = level;
    }

    public override string ToString()
    {
        return $"PlayerLevelUpSignal: {Level}";
    }

    public static implicit operator int(PlayerLevelUpSignal signal)
    {
        return signal.Level;
    }

    public static implicit operator PlayerLevelUpSignal(int level)
    {
        return new PlayerLevelUpSignal(level);
    }
}
