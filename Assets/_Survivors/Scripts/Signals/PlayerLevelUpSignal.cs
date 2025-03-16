using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public struct PlayerLevelUpSignal
{
    public int Level { get; }

    public List<UpgradeData> Upgrades { get; }

    public PlayerLevelUpSignal(int level, List<UpgradeData> upgrades = null)
    {
        Level = level;

        Upgrades = upgrades ?? new List<UpgradeData>();
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
