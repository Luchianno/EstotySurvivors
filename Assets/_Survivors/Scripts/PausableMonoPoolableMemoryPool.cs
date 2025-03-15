using UnityEngine;
using Zenject;
using System.Collections.Generic;

public abstract class PausableMonoPoolableMemoryPool<TParam1, TParam2, TParam3, TObject> :
    MonoPoolableMemoryPool<TParam1, TParam2, TParam3, IMemoryPool, TObject>
    where TObject : MonoBehaviour, IPoolable<TParam1, TParam2, TParam3, IMemoryPool>, IPausable
{
    private readonly HashSet<TObject> activeObjects = new();

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

    public void PauseAll()
    {
        foreach (var obj in activeObjects)
        {
            obj.SetPaused(true);
        }
    }

    public void ResumeAll()
    {
        foreach (var obj in activeObjects)
        {
            obj.SetPaused(false);
        }
    }
}