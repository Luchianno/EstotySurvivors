using UnityEngine;

public class BulletData : MonoBehaviour
{
    public string InternalName;

    public string DisplayName;
    public string Description;

    [Header("Stats")]
    [Min(1)]
    public int Damage = 2;
    [Min(0)]
    public float Speed = 10f;
    [Min(0)]
    public float LifeTime = 2f;
    public bool IsPiercing;
    public bool IsExplosive;

    [Space]
    public Sprite Icon;
    [Space]
    public ExplositonData ExplosionData;

    void OValidate()
    {
         
    }
}