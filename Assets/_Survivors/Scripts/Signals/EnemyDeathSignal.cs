using UnityEngine;

public struct EnemyDeathSignal
{
    public EnemyUnit EnemyUnit { get; }

    public EnemyDeathSignal(EnemyUnit enemyUnit)
    {
        EnemyUnit = enemyUnit;
    }
}