using UnityEngine;
using Zenject;

public class PlayerHealth : UnitHealth
{
    [SerializeField] Animator animator;

    [Inject] SignalBus signalBus;


    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        Current = Max;
    }

    public void Heal(int amount)
    {
        ChangeBy(new HealthChange(amount));
    }

    protected override void OnHeatlhChange(HealthChange change)
    {
        base.OnHeatlhChange(change);

        if (change.Amount < 0)
        {
            // animator.SetTrigger("Hit");

            signalBus.Fire(new PlayerDamageSignal(change.Amount, this.Current, this.Max));
        }
        if (change.Amount > 0)
        {
            signalBus.Fire(new PlayerHealSignal(change.Amount, this.Current, this.Max));
        }
    }

    protected override void OnDying()
    {
        base.OnDying();

        animator.SetBool("Dead", true);

        signalBus.Fire(new PlayerDeathSignal());

        // spriteRenderer.DOFade(0, deathStoneDuration).OnComplete(() =>
        // {
        //     enemyUnit.Dispose();
        // });
    }
}
