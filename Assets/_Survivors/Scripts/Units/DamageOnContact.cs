using System.Collections;
using UnityEngine;
using Zenject;

// when the player collides with an object that has this script, the player will take damage every interval
// we override both OnTriggerEnter2D and OnTriggerExit2D, because enemy movement and collision handling might change in future
public class DamageOnContact : MonoBehaviour
{
    const float damageInterval = 1;

    [SerializeField] EnemyUnit enemyUnit;

    WaitForSeconds wait = new WaitForSeconds(damageInterval);

    Coroutine damageCoroutine;

    PlayerHealth playerHealth;

    void OnDisable()
    {
        StopDamaging();
    }

    void OnTriggerEnter2D(Collider2D other) => OnEnter(other);
    void OnTriggerExit2D(Collider2D other) => OnExit(other);

    void OnColliderEnter2D(Collider2D other) => OnEnter(other);
    void OnColliderExit2D(Collider2D other) => OnExit(other);


    void OnEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerHealth == null)
                playerHealth = other.GetComponent<PlayerHealth>();

            damageCoroutine = StartCoroutine(DamagePlayer());
        }
    }

    void OnExit(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopDamaging();
        }
    }


    void StopDamaging()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator DamagePlayer()
    {
        while (true)
        {
            playerHealth.ChangeBy(new HealthChange(-enemyUnit.Stats.ContactDamage));
            yield return wait;
        }
    }
}
