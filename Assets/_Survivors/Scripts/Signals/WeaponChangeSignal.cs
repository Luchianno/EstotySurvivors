using UnityEngine;

public struct WeaponChangeSignal
{
    public WeaponData Data { get; }

    public WeaponChangeSignal(WeaponData data)
    {
        Data = data;
    }
}
