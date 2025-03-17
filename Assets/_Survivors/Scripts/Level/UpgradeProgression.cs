using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeProgression", menuName = "💀 Survivors/Upgrade Progression")]
public class UpgradeProgression : ScriptableObject
{
    [SerializeField] List<LevelUpgrades> upgrades = new List<LevelUpgrades>();

    public List<UpgradeData> GetUpgradesForLevel(int level)
    {
        if (level < 0 || level >= upgrades.Count)
        {
            Debug.LogError($"Level {level} is out of range. Returning empty list.");
            return new List<UpgradeData>();
        }

        return upgrades[level].Upgrades;
    }

    [Serializable]
    public class LevelUpgrades 
    {
        public List<UpgradeData> Upgrades;
    }

}