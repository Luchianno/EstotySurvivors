using UnityEngine;

public abstract class ScriptableUpgradeLogic : ScriptableWithDescription, IUpgradeLogic
{
    public abstract void ApplyUpgrade();
}