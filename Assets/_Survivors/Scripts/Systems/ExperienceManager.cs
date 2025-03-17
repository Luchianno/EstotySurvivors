using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExperienceManager : IInitializable
{
    // progress till next level. so we clamp it between previous and next level experience
    public float Progress
    {
        get
        {
            // Handle max level case
            if (levelProgression.IsMaxLevel)
                return 1f;

            var currentLevelExp = levelProgression.CurrentLevel.ExperienceRequired;
            var nextLevelExp = levelProgression.NextLevel.ExperienceRequired;


            return Mathf.Clamp01((float)(TotalExperience - currentLevelExp) / (float)(nextLevelExp - currentLevelExp));
        }
    }

    [Inject] SignalBus signalBus;
    [Inject] LevelProgression levelProgression;
    [Inject] UpgradeProgression upgradeProgression;

    public int TotalExperience { get; protected set; }

    public void Initialize()
    {
        signalBus.Subscribe<AddExperienceSignal>(AddExperience);
    }

    public void Reset()
    {
        TotalExperience = 0;
    }

    public void AddExperience(AddExperienceSignal signal) => AddExperience(signal.Amount);

    public void AddExperience(int amount)
    {
        if (amount <= 0)
            return;

        TotalExperience += amount;

        // in case we gained so much experience that we gained several levels at once
        var levelsGained = 0;

        while (!levelProgression.IsMaxLevel && TotalExperience >= levelProgression.NextLevel.ExperienceRequired)
        {
            levelProgression.CurrentLevelIndex++;
            levelsGained++;

            var levelUpgrades = upgradeProgression.GetUpgradesForLevel(levelProgression.CurrentLevelIndex);
            signalBus.Fire(new PlayerLevelUpSignal(levelProgression.CurrentLevelIndex + 1, levelUpgrades));
        }

        signalBus.Fire(new PlayerExperienceGainedSignal(amount, Progress, levelsGained));
    }
}
