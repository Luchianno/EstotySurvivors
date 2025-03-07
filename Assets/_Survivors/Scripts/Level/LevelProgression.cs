using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "DifficultyProgression", menuName = "💀 Survivors/Difficulty Progression")]
public class LevelProgression : ScriptableObject, ILevelProgression
{
    public List<ILevel> Levels => levels.Cast<ILevel>().ToList();

    public int CurrentLevelIndex { get; set; }

    public ILevel CurrentLevel => levels[CurrentLevelIndex];

    [SerializeField] List<Level> levels;

    void Awake()
    {
        Warmup();
    }

    void OnValidate()
    {
        Warmup();
    }

    void Warmup()
    {
        foreach (var level in levels)
        {
            level.Warmup();
        }
    }

}

