using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Level : ILevel
{
    [field: SerializeField, TextArea] public string LevelNotes { get; protected set; }
    [field: SerializeField] public int ExperienceRequired { get; protected set; }
    [field: SerializeField] public int MaxEnemies { get; protected set; }
    [SerializeField] List<EnemyWeightPair> EnemyProbability;
    WeightedRandomPicker<EnemyData> enemyPicker;

    public List<EnemyWeightPair> GetEnemyProbability() => EnemyProbability;

    // generates a list of cumulative weights for the enemies
    // so we can retrieve a random enemy type based on their weight more efficiently
    public void Warmup()
    {
        enemyPicker = new WeightedRandomPicker<EnemyData>(EnemyProbability.Cast<IWeightPair<EnemyData>>());
    }

    public EnemyData GetRandomEnemyType() => enemyPicker.Pick();
}