using UnityEngine;

public struct PlayerDamageSignal
{
    public int Amount { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }

    public PlayerDamageSignal(int damage, int currentHealth, int maxHealth)
    {
        this.Amount = Mathf.Abs(damage);
        this.CurrentHealth = currentHealth;
        this.MaxHealth = maxHealth;
    }

    public override string ToString()
    {
        return $"PlayerDamageSignal: {Amount}";
    }

    public static implicit operator int(PlayerDamageSignal signal)
    {
        return signal.Amount;
    }

}
