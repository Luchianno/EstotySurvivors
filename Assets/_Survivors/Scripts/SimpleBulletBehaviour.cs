using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SimpleBulletBehaviour : BasePausableBehaviour, IPoolable<Vector2, Vector2, BulletData, IMemoryPool>, IDisposable
{
    [field: SerializeField]
    public BulletData Data { get; protected set; }

    [SerializeField] Rigidbody2D body;
    [SerializeField] SpriteRenderer spriteRenderer;

    IMemoryPool pool;

    public override void SetPaused(bool isPaused)
    {
        base.SetPaused(isPaused);

        if (isPaused)
        {
            body.simulated = false;
        }
        else
        {
            body.simulated = true;
        }
    }

    public void OnSpawned(Vector2 position, Vector2 direction, BulletData data, IMemoryPool pool)
    {
        this.pool = pool;
        Data = data;

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, direction));

        spriteRenderer.sprite = data.Icon;

        body.simulated = true;
        body.linearVelocity = transform.up * data.Speed;

        StartCoroutine(DestroyAfter(data.Lifetime));

        // despawn after lifetime. but pause if this component is paused
        IEnumerator DestroyAfter(float time)
        {
            float elapsed = 0f;

            while (elapsed < time)
            {
                if (!this)
                    yield break;

                if (!this.enabled)
                    yield return null;

                elapsed += Time.deltaTime;

                yield return null;
            }
        }
    }

    public void OnDespawned()
    {
        pool = null;
        Data = null;

        StopAllCoroutines();
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

    public class Pool : PausableMonoPoolableMemoryPool<Vector2, Vector2, BulletData, SimpleBulletBehaviour> { }
}