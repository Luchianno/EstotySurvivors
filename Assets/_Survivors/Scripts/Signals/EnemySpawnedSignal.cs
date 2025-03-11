using UnityEngine;

public struct EnemySpawnedSignal 
{
    public EnemyUnit Enemy;

    public EnemySpawnedSignal(EnemyUnit enemy)
    {
        Enemy = enemy;
    }
}
