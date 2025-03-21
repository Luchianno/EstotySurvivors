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

    public UnityEvent<HealthChange> OnHealthChangeEvent;
    public UnityEvent<GameObject> OnDyingEvent;

    public void ChangeBy(HealthChange delta)
    {
        if (!IsAlive || delta.Amount == 0)
        {
            return;
        }

        if (IsInvinsible && delta.Amount < 0)
        {
            // if invinsible, ignore damage
            return;
        }

        Current += delta.Amount;
        Current = Mathf.Clamp(Current, 0, Max);

        OnHeatlhChange(delta);

        if (Current == 0)
        {
            OnDying();
        }
    }

    protected virtual void OnHeatlhChange(HealthChange change)
    {
        OnHealthChangeEvent.Invoke(change);
    }

    protected virtual void OnDying()
    {
        OnDyingEvent.Invoke(gameObject);
    }

    public virtual void ResetState()
    {
        Current = Max;
    }
}
