using UnityEngine;

public struct UpgradeSelectedSignal 
{
    public UpgradeData UpgradeData { get;  }

    public UpgradeSelectedSignal(UpgradeData upgradeData)
    {
        UpgradeData = upgradeData;
    }

}
