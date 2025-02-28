using UnityEngine;

[CreateAssetMenu(fileName = "PickupItemData", menuName = "Survivors/Pickup Item Data", order = 1)]
public class PickupItemData : MonoBehaviour
{
    public string InternalName;

    [Space]
    public string ItemName;
    public string Description;

    [Space]
    public Sprite Icon;
}
