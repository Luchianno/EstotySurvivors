using System;
using UnityEngine;

[Serializable]
public struct EnemyStats
{
    [field: SerializeField] public float Speed { get; }
    [field: SerializeField] public int MaxHealth { get; }
    [field: SerializeField] public int ContactDamage { get; }
    [field: SerializeField] public float EnemySize { get; }

    public EnemyStats(EnemyData data)
    {
        Speed = UnityEngine.Random.Range(data.SpeedMin, data.SpeedMax);
        MaxHealth = UnityEngine.Random.Range(data.HealthMin, data.HealthMax);
        ContactDamage = UnityEngine.Random.Range(data.ContactDamageMin, data.ContactDamageMax);
        EnemySize = UnityEngine.Random.Range(data.EnemySizeMin, data.EnemySizeMax);
    }


}
