using UnityEngine;

public class PlayerHealSignal
{
    public int Amount { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }

    public PlayerHealSignal(int heal, int current, int max)
    {
        if (heal < 0)
        {
            Debug.LogError("Heal amount cannot be negative");
            heal = 0;
        }
        Amount = heal;
        CurrentHealth = current;
        MaxHealth = max;
    }

    public override string ToString()
    {
        return $"PlayerHealSignal: {Amount}";
    }

    public static implicit operator int(PlayerHealSignal signal)
    {
        return signal.Amount;
    }

}
