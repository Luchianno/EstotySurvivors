using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    int GetMaxEnemies();
    List<WeightPair> GetEnemyProbability();

    EnemyData GetRandomEnemyType();
}
