using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "DifficultyProgression", menuName = "💀 Survivors/Difficulty Progression")]
public class LevelProgression : ScriptableObject, ILevelProgression
{
    public int CurrentLevelIndex
    {
        get => currentLevelIndex;
        set => currentLevelIndex = Mathf.Clamp(value, 0, levels.Count - 1);
    }
    public ILevel CurrentLevel => levels[CurrentLevelIndex];
    public ILevel NextLevel => CurrentLevelIndex < levels.Count - 1 ? levels[CurrentLevelIndex + 1] : default;
    public bool IsMaxLevel => CurrentLevelIndex == levels.Count - 1;
    public List<ILevel> Levels => levels.Cast<ILevel>().ToList();

    int currentLevelIndex = 0;

    [SerializeField] List<Level> levels;

    void Awake()
    {
        Warmup();
    }

    void OnValidate()
    {
        Warmup();

        // validate that level experience points are in ascending order
        for (int i = 1; i < levels.Count; i++)
        {
            if (levels[i].ExperienceRequired <= levels[i - 1].ExperienceRequired)
            {
                Debug.LogError($"Level {i} experience points must be greater than level {i - 1} experience points");
            }
        }
    }

    void Warmup()
    {
        foreach (var level in levels)
        {
            level.Warmup();
        }
    }

}

