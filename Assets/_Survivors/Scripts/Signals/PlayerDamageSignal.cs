using UnityEngine;

public struct PlayerDamageSignal
{
    public int Amount { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }

    public PlayerDamageSignal(int damage, int currentHealth, int maxHealth)
    {
        if (damage < 0)
        {
            Debug.LogError("Damage amount cannot be negative");
            damage = 0;
        }

        this.Amount = damage;
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
