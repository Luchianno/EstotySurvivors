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
            var previousLevelExp = levelProgression.CurrentLevelIndex > 0 ? levelProgression.Levels[levelProgression.CurrentLevelIndex - 1].ExperienceRequired : 0;
            var nextLevelExp = levelProgression.CurrentLevel.ExperienceRequired;

            return (float)(TotalExperience - previousLevelExp) / (nextLevelExp - previousLevelExp);
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
        TotalExperience += amount;

        var levelsGained = 0;

        while (TotalExperience >= levelProgression.CurrentLevel.ExperienceRequired)
        {
            levelProgression.CurrentLevelIndex++;
            levelsGained++;

            var levelUpgrades = upgradeProgression.GetUpgradesForLevel(levelProgression.CurrentLevelIndex);
            
            signalBus.Fire(new PlayerLevelUpSignal(levelProgression.CurrentLevelIndex + 1, levelUpgrades));

            if (levelProgression.CurrentLevelIndex == levelProgression.Levels.Count - 1)
            {
                break;
            }
        }

        signalBus.Fire(new PlayerExperienceGainedSignal(amount, Progress, levelsGained));
    }

}
