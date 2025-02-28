using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    [field: SerializeField]
    public int Current { get; protected set; }
    [field: SerializeField]
    public int Max { get; protected set; }

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent<GameObject> OnDying;

    public void Reset()
    {
        Current = Max;
    }

    public void Damage(int damage)
    {
        if(Current <= 0)
        {
            return;
        }

        Current -= damage;
        Current = Mathf.Max(0, Current);

        OnHealthChanged.Invoke(Current);

        if(Current == 0)
        {
            OnDying.Invoke(gameObject);
        }
    }

}
