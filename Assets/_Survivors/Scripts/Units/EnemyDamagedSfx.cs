using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyHealth), typeof(AudioSource))]
public class EnemyDamagedSfx : MonoBehaviour
{
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip criticalHit;

    [Space]
    [SerializeField] AudioSource source;
    [SerializeField] EnemyHealth health;

    [Inject] AppSettings appSettings;

    void Awake()
    {
        if (health == null)
        {
            health = GetComponent<EnemyHealth>();
        }

        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }

        health.OnHealthChangeEvent.AddListener(OnEnemyHit);
    }

    void OnEnemyHit(HealthChange healthChange)
    {
        if (healthChange.Amount < 0)
        {
            var clip = healthChange.Type switch
            {
                HealthChangeType.Normal => hit,
                HealthChangeType.Critical => criticalHit,
                _ => null
            };

            if (clip != null)
            {
                source.pitch = Random.Range(0.9f, 1.1f);
                source.PlayOneShot(clip, appSettings.SFXVolume);
            }
        }
    }


}
