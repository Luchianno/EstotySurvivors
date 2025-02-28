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

        health.OnHealthChanged.AddListener(OnHealthChanged);
        health.OnDying.AddListener(OnDying);
    }

    void OnHealthChanged(int current)
    {
        animator.SetTrigger("Hit");
    }

    void OnDying(GameObject gameObject)
    {
        animator.SetBool("IsDead", true);
    }
}
