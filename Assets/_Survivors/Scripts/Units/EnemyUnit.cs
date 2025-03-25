using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemyUnit : BasePausableBehaviour, IPoolable<Vector3, EnemyData, IMemoryPool>, IDisposable
{
    public bool IsAlive => (Health != null) && Health.IsAlive;

    [field: SerializeField]
    public EnemyStats Stats { get; protected set; }
    [field: SerializeField]
    public EnemyData Data { get; protected set; }

    [field: SerializeField]
    public EnemyHealth Health { get; protected set; }
    [field: SerializeField]
    public SpriteRenderer SpriteRenderer { get; protected set; }

    [SerializeField] Animator animator;

    IMemoryPool pool;
    // List of all components that we automatically reset when the enemy is spawned
    List<IResetState> resetables;
    List<IPausable> pausables;

    public override void SetPaused(bool isPaused)
    {
        base.SetPaused(isPaused);

        if (animator != null)
            animator.enabled = !isPaused;

        foreach (var pausable in pausables)
        {
            pausable.SetPaused(isPaused);
        }
    }

    public void OnSpawned(Vector3 position, EnemyData data, IMemoryPool pool)
    {
        this.pool = pool;
        this.Data = data;

        // Data has random values, so we need to create a new instance of EnemyStats
        // which will be used to store the actual stats of the enemy
        Stats = new EnemyStats(Data);

        transform.position = position;
        transform.localScale = Vector3.one * Stats.EnemySize;

        Health.Max = Stats.MaxHealth;
        animator.runtimeAnimatorController = Data.AnimatorController;

        if (resetables == null)
            resetables = GetComponentsInChildren<IResetState>(true).ToList();

        if (pausables == null)
            pausables = GetComponentsInChildren<IPausable>(true).ToList();

        foreach (var resetable in resetables)
        {
            resetable.ResetState();
        }
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void Dispose()
    {
        pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<Vector3, EnemyData, EnemyUnit> { }

}
