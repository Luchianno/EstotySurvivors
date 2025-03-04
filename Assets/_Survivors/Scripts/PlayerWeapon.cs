using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] Transform weaponSprite;

    [field: SerializeField]
    public WeaponData Data { get; protected set; }

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform target;
    [SerializeField] bool forceDisableAutoAim; // for debugging purposes

    [Inject] SimpleBulletBehaviour.Factory bulletFactory;

    float timeSinceLastShot = 0;
    List<Collider2D> closeEnemies = new List<Collider2D>();

    void Update()
    {
        target = FindClosestTarget();
        RotateWeapon();
        TryShootTarget();
    }

    Transform FindClosestTarget()
    {
        // find enemies in weapon range
        Physics2D.OverlapCircle(firePoint.position, Data.AutoAimRadius, new ContactFilter2D { layerMask = enemyLayer, useLayerMask = true }, closeEnemies);

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
                closestSqrMagnitude = sqrMagnitude;
                closestTarget = enemy.transform;
            }
        }

        return closestTarget;
    }

    void RotateWeapon()
    {
        if (target == null)
        {
            weaponSprite.DOLocalRotate(Vector3.zero, 0.3f);
            return;
        }

        Vector2 direction = target.position - weaponSprite.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponSprite.Rotate(Vector3.forward, angle);
    }

    void TryShootTarget()
    {
        if (target == null)
        {
            return;
        }

        if (timeSinceLastShot < Data.FireRate)
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        if (Data == null)
        {
            return;
        }

        var bullet = bulletFactory.Create(firePoint.position, weaponSprite.rotation.eulerAngles, Data.BulletData);

    }

    void ChangeWeapon(WeaponData data)
    {
        Data = data;

        weaponSprite.gameObject.SetActive(Data != null);
    }
}
