using UnityEngine;

public struct EnemyDamageSignal
{
    public EnemyUnit EnemyUnit { get; }
    public int Damage { get; }
    public bool IsCritical { get; }

    public EnemyDamageSignal(EnemyUnit enemyUnit, HealthChange change)
    {
        EnemyUnit = enemyUnit;
        Damage = change.Amount;
        IsCritical = change.Type == HealthChangeType.Critical;
    }

    public EnemyDamageSignal(EnemyUnit enemyUnit, int damage, bool isCritical)
    {
        EnemyUnit = enemyUnit;
        Damage = damage;
        IsCritical = isCritical;
    }
}
