using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "💀 Survivors/Bullet Data")]
public class BulletData : ScriptableWithDescription
{
    [Header("Stats")]
    [Min(1)]
    public int DamageMin = 2;
    [Min(1)]
    public int DamageMax = 5;
    [Range(0, 1)]
    public float CriticalChance = 0.01f;
    [Range(1, 10)]
    public float CriticalMultiplier = 2f;
    [Min(0)]
    public float Speed = 10f;
    [Min(0)]
    public float LifeTime = 2f;
    public bool IsPiercing;
    public bool IsExplosive;

    [Space]
    public Sprite Icon;
    [Space]
    public GameObject ExplosionPrefab;

    // returns absolute amount of health change, so it should be negative
    public HealthChange GetDamage()
    {
        int damage = Random.Range(DamageMin, DamageMax + 1);
        var healthChangeType = HealthChangeType.Normal;

        if (Random.value < CriticalChance)
        {
            damage = (int)(damage * CriticalMultiplier);
            healthChangeType = HealthChangeType.Critical;
        }

        return new HealthChange(-damage, healthChangeType);
    }

    void OValidate()
    {
        if (DamageMin > DamageMax)
        {
            DamageMin = DamageMax;
        }
    }
}