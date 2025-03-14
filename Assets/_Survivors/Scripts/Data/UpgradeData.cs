using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "💀 Survivors/Upgrade Data")]
public class UpgradeData : ScriptableWithDescription
{
    [Space]
    public UpgradeType Type;
    public Sprite Icon;
    public IUpgradeLogic UpgradeLogic;
}
