using System.Collections;
using UnityEngine;
using Zenject;

public class SimpleBulletBehaviour : MonoBehaviour
{
    public BulletData Data { get; protected set; }

    [SerializeField] new Rigidbody2D rigidbody2D;
    [SerializeField] SpriteRenderer spriteRenderer;

    public void Initialize(Vector3 position, Vector3 direction, BulletData data)
    {
        transform.position = position;
        transform.forward = direction;
        Data = data;

        spriteRenderer.sprite = data.Icon;

        rigidbody2D.linearVelocity = direction * data.Speed;

        StartCoroutine(DestroyAfter(data.LifeTime));

        IEnumerator DestroyAfter(float time)
        {
            yield return new WaitForSeconds(time);
            OnDespawn();
        }
    }

    void OnDespawn()
    {
        
    }

    // memory pool 
    public class Pool : MonoMemoryPool<Vector3, Vector3, BulletData, SimpleBulletBehaviour>
    {
        protected override void Reinitialize(Vector3 position, Vector3 direction, BulletData data, SimpleBulletBehaviour bullet)
        {
            bullet.Initialize(position, direction, data);
        }
    }
}
