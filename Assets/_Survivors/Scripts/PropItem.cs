using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PropItem : MonoBehaviour, IPoolable<Vector2, PropData, IMemoryPool>, IDisposable
{
    [SerializeField] PropData Data;

    [Space]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Inject] SignalBus signalBus;

    IMemoryPool pool;

    // Not checking for player, the collisions are controlled through Physics Settings.
    // So the props only will collide/trigger with the player
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        signalBus.Fire(new PropPickedSignal(Data));

        switch (Data.Type)
        {
            case PropType.Health:
                other.GetComponent<PlayerHealth>().Heal(Data.Value);
                StartCoroutine(OnPickup());
                break;
            case PropType.Ammo:
                other.GetComponent<PlayerWeapon>().AddAmmo(Data.Value);
                StartCoroutine(OnPickup());
                break;
            case PropType.Gem:
                signalBus.Fire(new AddExperienceSignal(Data.Value, "Gem collected"));
                StartCoroutine(OnPickup());
                break;
            default:
                Debug.LogError("Unhandled PropType: " + Data.Type);
                break;
        }
    }

    protected virtual IEnumerator OnPickup()
    {
        // play animations and stuff, then dispose
        spriteRenderer.transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(Dispose);

        yield return null;
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void OnSpawned(Vector2 position, PropData data, IMemoryPool pool)
    {
        this.pool = pool;
        Data = data;
        spriteRenderer.sprite = data.Icon;

        transform.position = position;
    }

    public void Dispose()
    {
        pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<Vector2, PropData, PropItem>
    {
    }
}