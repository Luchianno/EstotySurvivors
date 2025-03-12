using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    int ExperienceRequired { get; }
    int MaxEnemies { get; }
    List<EnemyWeightPair> GetEnemyProbability();

    EnemyData GetRandomEnemyType();
}
