using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float deathStoneDuration = 5f;
    [SerializeField] EnemyUnit enemyUnit;
    [SerializeField] UnitHealth health;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    WaitForSeconds waitForDeathStone;

    void Awake()
    {
        if (enemyUnit == null)
            enemyUnit = GetComponent<EnemyUnit>();
        if (health == null)
            health = GetComponent<UnitHealth>();
        if (animator == null)
            animator = GetComponent<Animator>();

        health.OnHealthChange.AddListener(OnHealthChanged);
        health.OnDyingEvent.AddListener(OnDying);

        waitForDeathStone = new WaitForSeconds(deathStoneDuration);
    }

    void OnHealthChanged(HealthChange change)
    {
        if (change.Amount < 0)
        {
            animator.SetTrigger("Hit");
        }
    }

    void OnDying(GameObject gameObject)
    {
        animator.SetBool("Dead", true);

        spriteRenderer.DOFade(0, deathStoneDuration).OnComplete(() =>
        {
            enemyUnit.Dispose();
        });
    }
}
