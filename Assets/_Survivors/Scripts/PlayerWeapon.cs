using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerWeapon : MonoBehaviour
{
    public bool HasTarget => Target != null;

    [field: SerializeField]
    public Transform Target { get; protected set; }

    [field: SerializeField]
    public WeaponData Data { get; protected set; }

    [SerializeField] Transform firePoint;
    [SerializeField] Transform weaponSprite;
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] bool forceDisableAutoAim; // for debugging purposes

    [Inject] SimpleBulletBehaviour.Factory bulletFactory;

    float timeSinceLastShot = 0;
    List<Collider2D> closeEnemies = new List<Collider2D>();

    void Update()
    {
        Target = FindClosestTarget();
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
            weaponSprite.DOLocalRotate(Vector3.zero, 0.3f);
            return;
        }

        // snap 2d sprite of weapon so that it points towards the target
        Vector2 direction = Target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponSprite.DOLocalRotate(new Vector3(0, 0, angle), 0.3f);
    }

    void TryShootTarget()
    {
        if (Target == null)
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
