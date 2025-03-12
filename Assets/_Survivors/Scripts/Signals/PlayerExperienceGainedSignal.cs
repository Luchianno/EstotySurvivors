using UnityEngine;

public struct PlayerExperienceGainedSignal
{
    public int Amount { get; }

    // progress till next level
    public float Progress { get; }

    public int LevelsGained { get; }

    public PlayerExperienceGainedSignal(int experienceGained, float percentageTillNextLevel, int levelsGained)
    {
        Amount = experienceGained;
        Progress = percentageTillNextLevel;
        LevelsGained = levelsGained;
    }
}