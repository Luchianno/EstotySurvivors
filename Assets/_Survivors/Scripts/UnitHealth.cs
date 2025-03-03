using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour, IResetState
{
    public bool IsAlive => Current > 0;
    [field: SerializeField]
    public bool IsInvinsible { get; set; }
    [field: SerializeField]
    public int Current { get; protected set; }
    [field: SerializeField]
    public int Max { get; set; }

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent<GameObject> OnDying;

    public void Damage(int damage)
    {
        if (Current <= 0)
        {
            return;
        }

        if (IsInvinsible)
        {
            return;
        }

        Current -= damage;
        Current = Mathf.Max(0, Current);

        OnHealthChanged.Invoke(Current);

        if (Current == 0)
        {
            OnDying.Invoke(gameObject);
        }
    }

    public void ResetState()
    {
        Current = Max;
    }
}
