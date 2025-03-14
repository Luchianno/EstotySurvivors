using UnityEngine;

[CreateAssetMenu(fileName = "PickupItemData", menuName = "💀 Survivors/Prop Data", order = 1)]
public class PropData : ScriptableWithDescription
{
    [Header("For Gameplay")]
    public PropType Type;
    public Sprite Icon;
    public int Value;
}
