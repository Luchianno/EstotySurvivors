using UnityEngine;

public class PlayerHealSignal
{
    public int Previous { get; }
    public int Amount { get; }
    public int Current { get; }
    public int Max { get; }

    public PlayerHealSignal(int heal, int previous, int current, int max)
    {
        if (heal < 0)
        {
            Debug.LogError("Heal amount cannot be negative");
            heal = 0;
        }
        Amount = heal;
        Previous = previous;
        Current = current;
        Max = max;
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
