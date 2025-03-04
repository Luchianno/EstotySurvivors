using UnityEngine;

public struct PlayerDamageSignal
{
    public int Damage { get; }

    public PlayerDamageSignal(int damage)
    {
        Damage = damage;
    }

    public override string ToString()
    {
        return $"PlayerDamageSignal: {Damage}";
    }

    public static implicit operator int(PlayerDamageSignal signal)
    {
        return signal.Damage;
    }

    public static implicit operator PlayerDamageSignal(int damage)
    {
        return new PlayerDamageSignal(damage);
    }
}
