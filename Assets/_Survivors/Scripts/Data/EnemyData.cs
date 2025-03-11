using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyData", menuName = "💀 Survivors/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string InternalName;

    [Space]
    public string DisplayName;
    [TextArea]
    public string Description;

    [Space]
    [Range(1f, 10f)]
    public float SizeMin = 1f;
    [Range(1f, 10f)]
    public float SizeMax = 1f;

    [Min(0)]
    public float SpeedMin = 10f;
    [Min(0)]
    public float SpeedMax = 10f;
    [Space]
    [Min(1)]
    public int HealthMin;
    [Min(1)]
    public int HealthMax;
    [Space]
    [Min(0)]
    public int ContactDamageMin = 1;
    [Min(0)]
    public int ContactDamageMax = 1;
    [Space, FormerlySerializedAs("Animator")]
    public AnimatorOverrideController AnimatorController;

    void OnValidate()
    {
        if (SpeedMin > SpeedMax)
        {
            SpeedMax = SpeedMin;
        }

        if (HealthMin > HealthMax)
        {
            HealthMax = HealthMin;
        }

        if (ContactDamageMin > ContactDamageMax)
        {
            ContactDamageMax = ContactDamageMin;
        }
    }
}
