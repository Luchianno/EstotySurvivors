using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUDView : MonoBehaviour
{
    [SerializeField] float healthAnimationDuration = 0.3f;
    [SerializeField] float xpAnimationDuration = 0.5f;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider xpBar;
    [SerializeField] TextMeshProUGUI killCountText;

    [Inject] SignalBus signalBus;

    void Start()
    {
        signalBus.Subscribe<PlayerDamageSignal>(OnPlayerDamage);
        signalBus.Subscribe<PlayerHealSignal>(OnPlayerHeal);

        signalBus.Subscribe<ScoreChangedSignal>(UpdateKillCounter);
        signalBus.Subscribe<PlayerExperienceGainedSignal>(OnPlayerExperienceGained);

        Reset();
    }

    void Reset()
    {
        healthBar.SetValueWithoutNotify(1);
        xpBar.SetValueWithoutNotify(0);
        killCountText.text = "0";
    }


    void OnPlayerDamage(PlayerDamageSignal signal)
    {
        healthBar.DOValue((float)signal.CurrentHealth / signal.MaxHealth, healthAnimationDuration)
            .SetEase(Ease.OutCubic);
    }

    void OnPlayerHeal(PlayerHealSignal signal)
    {
        healthBar.DOValue((float)signal.CurrentHealth / signal.MaxHealth, healthAnimationDuration)
            .SetEase(Ease.OutBounce);
    }

    void UpdateKillCounter(ScoreChangedSignal signal)
    {
        killCountText.text = signal.Score.ToString();

        // animate label
        killCountText.transform.DOPunchScale(Vector3.one * 0.4f, 0.5f);
    }

    void OnPlayerExperienceGained(PlayerExperienceGainedSignal signal)
    {
        // dotween sequence to animate xp bar
        var sequence = DOTween.Sequence();

        // fill the xp bar as many times as levels gained
        for (int i = 0; i < signal.LevelsGained; i++)
        {
            sequence.Append(xpBar.DOValue(1, xpAnimationDuration).SetEase(Ease.OutCubic));
            sequence.AppendInterval(0.5f);
            sequence.Append(xpBar.DOValue(0, 0));
        }

        // fill the xp bar to the current progress
        sequence.Append(xpBar.DOValue(signal.Progress, xpAnimationDuration).SetEase(Ease.OutCubic));

        sequence.Play();



    }

}
