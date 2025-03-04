using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemyUnit : MonoBehaviour, IPoolable<Vector3, EnemyData, IMemoryPool>, IDisposable
{
    public bool IsAlive => (Health != null) && Health.IsAlive;

    [field: SerializeField]
    public EnemyStats Stats { get; protected set; }
    [field: SerializeField]
    public EnemyData Data { get; protected set; }

    [field: SerializeField]
    public UnitHealth Health { get; protected set; }

    [SerializeField] Animator animator;

    List<IResetState> resetables;

    IMemoryPool pool;

    public void OnSpawned(Vector3 position, EnemyData data, IMemoryPool pool)
    {
        this.pool = pool;
        this.Data = data;

        // Data has random values, so we need to create a new instance of EnemyStats
        // which will be used to store the actual stats of the enemy
        Stats = new EnemyStats(Data);

        transform.position = position;
        transform.localScale = Vector3.one * Data.EnemySizeMin;

        Health.Max = Stats.MaxHealth;
        animator.runtimeAnimatorController = Data.AnimatorController;

        if (resetables == null)
            resetables = GetComponentsInChildren<IResetState>().ToList();

        foreach (var resetable in resetables)
        {
            resetable.ResetState();
        }
    }

    public void OnDespawned() { }

    public void Dispose()
    {
        pool = null;
    }

    public class Factory : PlaceholderFactory<Vector3, EnemyData, EnemyUnit> { }

}
