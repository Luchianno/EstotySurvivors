using UnityEngine;

[CreateAssetMenu(fileName = "PickupItemData", menuName = "💀 Survivors/Prop Data", order = 1)]
public class PropData : ScriptableObject
{
    public string InternalName;

    [Header("For UI and Description")]
    public string DisplayName;
    [TextArea]
    public string Description;

    [Header("For Gameplay")]
    public PropType Type;
    public Sprite Icon;
    public int Value;
}
