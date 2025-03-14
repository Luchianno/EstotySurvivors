using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class SimpleBulletBehaviour : MonoBehaviour, IPoolable<Vector2, Vector2, BulletData, IMemoryPool>, IDisposable
{
    [field: SerializeField]
    public BulletData Data { get; protected set; }

    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer spriteRenderer;

    IMemoryPool pool;

    public void OnSpawned(Vector2 position, Vector2 direction, BulletData data, IMemoryPool pool)
    {
        this.pool = pool;
        Data = data;

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, direction));

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
        Data = null;
    }

    public void Dispose()
    {
        pool.Despawn(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) => OnEnter(other);
    protected virtual void OnColliderEnter2D(Collider2D other) => OnEnter(other);

    protected virtual void OnEnter(Collider2D other)
    {
        if (!this.gameObject.activeSelf)
            return;

        var otherHealth = other.GetComponent<UnitHealth>();

        if (otherHealth == null || !otherHealth.IsAlive)
            return;

        otherHealth.ChangeBy(Data.GetDamage());

        // keep flying through. 
        // TODO maybe add max number of pierces
        if (Data.IsPiercing)
            return;

        Dispose();
    }

    public class Factory : PlaceholderFactory<Vector2, Vector2, BulletData, SimpleBulletBehaviour>
    {
    }
}