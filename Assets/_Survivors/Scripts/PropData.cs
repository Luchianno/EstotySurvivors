using UnityEngine;

[CreateAssetMenu(fileName = "PickupItemData", menuName = "Survivors/Prop Data", order = 1)]
public class PropData : MonoBehaviour
{
    public string InternalName;

    [Space]
    public string ItemName;
    public string Description;

    [Space]
    public Sprite Icon;
}
