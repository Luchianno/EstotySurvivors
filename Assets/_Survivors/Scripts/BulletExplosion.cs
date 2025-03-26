using System;
using UnityEngine;
using Zenject;

public class BulletExplosion : MonoBehaviour, IPoolable<Vector2, IMemoryPool>, IDisposable
{
    [SerializeField] BulletData bulletData;
    
    [Inject] SimpleBulletBehaviour.Pool bulletFactory;
    [Inject] SignalBus signalBus;

    IMemoryPool pool;

    public void OnSpawned(Vector2 position, IMemoryPool pool)
    {
        this.pool = pool;
        transform.position = position;

        // fire 8 bullets in a circle
        for (int i = 0; i < 8; i++)
        {
            var direction = Quaternion.Euler(0, 0, i * 45) * Vector3.right;
            var bullet = bulletFactory.Create(position, direction, bulletData);
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

    public class Factory : PlaceholderFactory<Vector2, BulletExplosion> { }

}
