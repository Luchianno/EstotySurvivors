using UnityEngine;

[CreateAssetMenu(fileName = "WaeponData", menuName = "💀 Survivors/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string InternalName;

    public string DisplayName;
    [TextArea]
    public string Description;

    [Header("Stats")]
    [Min(0)]
    public float AutoAimRadius;
    [Min(0)]
    public float ShootInterval = 1f;
    [Range(1, 100)]
    public int BulletsPerShot;
    [Tooltip("In degrees")]
    [Range(0, 360)]
    public float SpreadDegrees;
    [Min(0)]
    public float ReloadTime = 1f;
    [Min(1)]
    public int ClipSize;
    [Min(1)]
    public int MaxAmmo;

    [Space]
    public Sprite Icon;

    [Space]
    public BulletData BulletData;

    void OValidate()
    {

        if (ClipSize > MaxAmmo)
        {
            ClipSize = MaxAmmo;
        }
    }
}