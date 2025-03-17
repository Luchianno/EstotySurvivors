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
    [SerializeField] float pickupAnimationDuration = 0.3f;

    [Inject] SignalBus signalBus;

    IMemoryPool pool;

    // Not checking for player, the collisions are controlled through Physics Settings.
    // So the props only will collide/trigger with the player
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeSelf)
            return;

        if (!other.CompareTag("Player"))
        {
            // Debug.LogError("PropItem collided with non-player object: " + other.name, this.gameObject);
            // Debug.LogError("Prop Layer: " + LayerMask.LayerToName(gameObject.layer) + " other: " + LayerMask.LayerToName(other.gameObject.layer), this.gameObject);
            return;
        }

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
        spriteRenderer.transform.DOPunchScale(Vector3.one * 2f, pickupAnimationDuration, 1, 0.5f)
            // .SetEase(Ease.InBounce)
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

        gameObject.name = data.InternalName;
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