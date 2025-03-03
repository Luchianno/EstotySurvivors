using UnityEngine;

[CreateAssetMenu(fileName = "WaeponData", menuName = "💀 Survivors")]
public class WeaponData : ScriptableObject
{
    public string InternalName;

    public string DisplayName;
    public string Description;

    [Header("Stats")]
    [Min(0)]
    public int MinDamage = 2;
    [Min(0)]
    public int MaxDamage = 2;
    public float AutoAimRadius;
    public float FireRate;
    [Min(0)]
    public float ReloadTime = 1f;
    [Min(1)]
    public int ClipSize;
    [Min(1)]
    public int MaxAmmo;

    void OValidate()
    {
        if(MaxDamage < MinDamage)
        {
            MaxDamage = MinDamage;
        }

        if(ClipSize > MaxAmmo)
        {
            ClipSize = MaxAmmo;
        }
    }
}