using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "💀 Survivors/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string InternalName;

    [Space]
    public string EnemyName;
    [TextArea]
    public string Description;

    [Space]
    [Range(1f, 10f)]
    public float EnemySize = 1f;
    public float SpeedMin;
    public float SpeedMax;
    [Space]
    public int MaxHealth;
    [Space]
    public int ContactDamageMin;
    public int ContactDamageMax;
    [Space]
    public AnimatorOverrideController Animator;
}
