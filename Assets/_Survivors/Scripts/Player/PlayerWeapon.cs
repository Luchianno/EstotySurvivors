using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerWeapon : MonoBehaviour
{
    public bool HasTarget => Target != null;
    public bool IsPlayerFlipped => transform.localScale.x < 0;
    public int Ammo { get; protected set; }

    [Header("If ticked,\ntarget can be assigned manually for testing")]
    public bool DisableAutoTargetSelection; // for debugging purposes
    public Transform Target;

    [field: SerializeField]
    public WeaponData WeaponData { get; protected set; }

    [Space]
    [SerializeField] Transform firePoint;
    [SerializeField] SpriteRenderer weaponSprite;
    [SerializeField] LayerMask enemyLayer;


    [Inject] SimpleBulletBehaviour.Factory bulletFactory;

    float timeSinceLastShot = 0;
    List<Collider2D> closeEnemies = new List<Collider2D>();

    void Update()
    {
        if (!DisableAutoTargetSelection)
            Target = FindClosestTarget();
        RotateWeapon();
        TryShootTarget();
    }

    Transform FindClosestTarget()
    {
        // find enemies in weapon range
        Physics2D.OverlapCircle(firePoint.position, WeaponData.AutoAimRadius, new ContactFilter2D { layerMask = enemyLayer, useLayerMask = true, useTriggers = true }, closeEnemies);

        if (closeEnemies.Count == 0)
        {
            return null;
        }

        // find the closest enemy without generating garbage and without using Distance() method
        Transform closestTarget = null;
        var closestSqrMagnitude = float.MaxValue;
        var currentPosition = transform.position;

        foreach (var enemy in closeEnemies)
        {
            Vector2 directionToTarget = enemy.transform.position - currentPosition;
            float sqrMagnitude = directionToTarget.sqrMagnitude;

            if (sqrMagnitude < closestSqrMagnitude)
            {
                var health = enemy.GetComponent<UnitHealth>();
                if (health != null && !health.IsAlive)
                {
                    continue;
                }

                closestSqrMagnitude = sqrMagnitude;
                closestTarget = enemy.transform;
            }
        }

        return closestTarget;
    }

    void RotateWeapon()
    {
        if (Target == null)
        {
            weaponSprite.transform.DOLocalRotate(Vector3.zero, 0.15f);
            return;
        }

        var direction = (Target.position - transform.position).normalized;
        direction = IsPlayerFlipped ? -direction : direction;

        // rotate weapon towards the target
        var foo = Vector2.MoveTowards(weaponSprite.transform.right, direction, 0.1f);

        weaponSprite.transform.right = foo;
    }

    void TryShootTarget()
    {
        if (Target == null)
        {
            return;
        }

        if (timeSinceLastShot < WeaponData.ShootInterval)
        {
            timeSinceLastShot += Time.deltaTime;
            return;
        }

        Shoot();
    }

    public void Shoot()
    {
        if (WeaponData == null)
        {
            return;
        }

        // shoot 2D bullets in the direction of the weapon
        for (int i = 0; i < WeaponData.BulletsPerShot; i++)
        {
            var gunForward = weaponSprite.transform.right;
            var spread = Random.Range(-WeaponData.SpreadDegrees / 2, WeaponData.SpreadDegrees / 2);
            var direction = Quaternion.Euler(0, 0, spread) * gunForward;

            direction = IsPlayerFlipped ? -direction : direction;

            var bullet = bulletFactory.Create(firePoint.position, direction, WeaponData.BulletData);
        }

        timeSinceLastShot = 0;
    }

    // TODO not implemented
    public void AddAmmo(int amount)
    {
        Ammo += amount;
    }

    // TODO not implemented
    public void ChangeWeapon(WeaponData data)
    {
        WeaponData = data;

        weaponSprite.gameObject.SetActive(WeaponData != null);
    }


}
