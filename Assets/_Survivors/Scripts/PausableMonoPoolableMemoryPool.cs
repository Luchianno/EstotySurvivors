using UnityEngine;
using Zenject;
using System.Collections.Generic;

// two params
public abstract class PausableMonoPoolableMemoryPool<TParam1, TParam2, TObject> :
    MonoPoolableMemoryPool<TParam1, TParam2, IMemoryPool, TObject>,
    IFactory<TParam1, TParam2, TObject>,
    IPausable
    where TObject : MonoBehaviour, IPoolable<TParam1, TParam2, IMemoryPool>, IPausable
{
    readonly HashSet<TObject> activeObjects = new();

    protected override void OnCreated(TObject item)
    {
        base.OnCreated(item);
    }

    protected override void OnSpawned(TObject item)
    {
        base.OnSpawned(item);
        activeObjects.Add(item);
    }

    protected override void OnDespawned(TObject item)
    {
        base.OnDespawned(item);
        activeObjects.Remove(item);
    }

    public void SetPaused(bool isPaused)
    {
        foreach (var item in activeObjects)
        {
            item.SetPaused(isPaused);
        }
    }

    public TObject Create(TParam1 param1, TParam2 param2) => Spawn(param1, param2, this);

}

// three params
public abstract class PausableMonoPoolableMemoryPool<TParam1, TParam2, TParam3, TObject> :
    MonoPoolableMemoryPool<TParam1, TParam2, TParam3, IMemoryPool, TObject>,
    IFactory<TParam1, TParam2, TParam3, TObject>,
    IPausable
    where TObject : MonoBehaviour, IPoolable<TParam1, TParam2, TParam3, IMemoryPool>, IPausable
{
    readonly HashSet<TObject> activeObjects = new();

    protected override void OnCreated(TObject item)
    {
        base.OnCreated(item);
    }

    protected override void OnSpawned(TObject item)
    {
        base.OnSpawned(item);
        activeObjects.Add(item);
    }

    protected override void OnDespawned(TObject item)
    {
        base.OnDespawned(item);
        activeObjects.Remove(item);
    }

    public void SetPaused(bool isPaused)
    {
        foreach (var item in activeObjects)
        {
            item.SetPaused(isPaused);
        }
    }

    public TObject Create(TParam1 param1, TParam2 param2, TParam3 param3) => Spawn(param1, param2, param3, this);
}