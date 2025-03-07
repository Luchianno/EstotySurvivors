using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    int GetMaxEnemies();
    List<EnemyWeightPair> GetEnemyProbability();

    EnemyData GetRandomEnemyType();
}
