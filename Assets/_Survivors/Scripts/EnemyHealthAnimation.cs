using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class EnemyHealthAnimation : MonoBehaviour
{
    [SerializeField] UnitHealth health;
    [SerializeField] Animator animator;

    void Awake()
    {
        if (health == null)
            health = GetComponent<UnitHealth>();
        if (animator == null)
            animator = GetComponent<Animator>();

        health.OnHealthChange.AddListener(OnHealthChanged);
        health.OnDying.AddListener(OnDying);
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
        animator.SetBool("IsDead", true);
    }
}
