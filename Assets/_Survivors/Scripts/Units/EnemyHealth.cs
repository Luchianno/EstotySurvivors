using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemyHealth : UnitHealth
{
    [SerializeField] float deathStoneDuration = 5f;
    [SerializeField] EnemyUnit enemyUnit;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    [Inject] SignalBus signalBus;

    WaitForSeconds waitForDeathStone;

    void Awake()
    {
        if (enemyUnit == null)
            enemyUnit = GetComponent<EnemyUnit>();
        if (animator == null)
            animator = GetComponent<Animator>();

        waitForDeathStone = new WaitForSeconds(deathStoneDuration);
    }


    protected override void OnHeatlhChange(HealthChange change)
    {
        base.OnHeatlhChange(change);

        if (change.Amount < 0)
        {
            animator.SetTrigger("Hit");
            
            signalBus.Fire(new EnemyDamageSignal(enemyUnit, change));
        }
    }

    protected override void OnDying()
    {
        base.OnDying();

        animator.SetBool("Dead", true);
        
        signalBus.Fire(new EnemyDeathSignal(enemyUnit));

        spriteRenderer.DOFade(0, deathStoneDuration).OnComplete(() =>
        {
            enemyUnit.Dispose();
        });
    }
}
