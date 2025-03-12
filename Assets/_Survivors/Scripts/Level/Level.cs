using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Level : ILevel
{
    [field: SerializeField] public int ExperienceRequired { get; protected set; }
    [field: SerializeField] public int MaxEnemies { get; protected set; }
    [SerializeField] List<EnemyWeightPair> EnemyProbability;

    public List<EnemyWeightPair> GetEnemyProbability() => EnemyProbability;

    List<float> cumulativeWeights = new List<float>();

    // generates a list of cumulative weights for the enemies
    // so we can retrieve a random enemy type based on their weight more efficiently
    public void Warmup()
    {
        float totalWeight = 0;
        foreach (var pair in EnemyProbability)
        {
            cumulativeWeights.Add(totalWeight);
        }

        totalWeight = cumulativeWeights[^1];
    }

    public EnemyData GetRandomEnemyType()
    {
        float totalWeight = cumulativeWeights[cumulativeWeights.Count - 1];
        float randomValue = Random.value * totalWeight;

        int selectedIndex = cumulativeWeights.BinarySearch(randomValue);
        if (selectedIndex < 0)
        {
            selectedIndex = ~selectedIndex;
        }

        return EnemyProbability[selectedIndex].Data;
    }
}