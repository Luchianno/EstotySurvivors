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
    [SerializeField] SpriteRenderer shadowRenderer;
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

    public override void ResetState()
    {
        base.ResetState();

        animator.SetBool("Dead", false);
        spriteRenderer.DOFade(1, 0);
        shadowRenderer.DOFade(1, 0);
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
        shadowRenderer.DOFade(0, 0.3f);
        
        signalBus.Fire(new EnemyDeathSignal(enemyUnit));

        spriteRenderer.DOFade(0, deathStoneDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
        {
            enemyUnit.Dispose();
        });
    }
}
