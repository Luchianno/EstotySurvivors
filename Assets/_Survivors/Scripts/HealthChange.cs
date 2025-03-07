using UnityEngine;

public struct HealthChange
{
    public int Amount { get; }
    public HealthChangeType Type { get; }
    // public GameObject Source;
    // public GameObject Target;

    public HealthChange(int amount, HealthChangeType type = HealthChangeType.Normal)
    {
        Amount = amount;
        Type = type;
    }

    public static int operator +(HealthChange healthChange, int amount)
    {
        return healthChange.Amount + amount;
    }

    public static int operator -(HealthChange healthChange, int amount)
    {
        return healthChange.Amount - amount;
    }

}
