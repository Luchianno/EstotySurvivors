using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class SimpleBulletBehaviour : MonoBehaviour, IPoolable<Vector3, Vector3, BulletData, IMemoryPool>, IDisposable
{
    [field: SerializeField]
    public BulletData Data { get; protected set; }

    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer spriteRenderer;

    IMemoryPool pool;

    public void OnSpawned(Vector3 position, Vector3 rotation, BulletData data, IMemoryPool pool)
    {
        this.pool = pool;
        Data = data;

        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);

        spriteRenderer.sprite = data.Icon;

        body.linearVelocity = transform.up * data.Speed;

        StartCoroutine(DestroyAfter(data.LifeTime));

        IEnumerator DestroyAfter(float time)
        {
            yield return new WaitForSeconds(time);
            Dispose();
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnColliderEnter2D(other);
    }

    protected virtual void OnColliderEnter2D(Collider2D other)
    {
        var otherHealth = other.GetComponent<UnitHealth>();

        if (otherHealth == null)
        {
            return;
        }

        otherHealth.Damage(Data.GetDamage());

        if (Data.IsPiercing)
        {
            return;
        }

        Dispose();
    }

    public class Factory : PlaceholderFactory<Vector3, Vector3, BulletData, SimpleBulletBehaviour>
    {
    }
}