using UnityEngine;

[CreateAssetMenu(fileName = "ExplositonData", menuName = "💀 Survivors")]
public class ExplositonData : ScriptableObject
{
    public string InternalName;

    public string DisplayName;
    public string Description;

    [Header("Stats")]
    [Min(0)]
    public float Radius = 2f;
    [Min(0)]
    public float LifeTime = 2f;

    void OValidate()
    {
    }
}
