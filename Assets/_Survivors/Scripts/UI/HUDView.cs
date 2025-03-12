using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUDView : MonoBehaviour
{
    const string levelPrefix = "Lv.";

    [Space]
    [SerializeField] float healthAnimationDuration = 0.3f;
    [SerializeField] float xpAnimationDuration = 0.5f;
    [SerializeField] float labelsAnimationDuration = 0.5f;

    [Space]
    [SerializeField] TextMeshProUGUI killCountLabel;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider xpBar;
    [SerializeField] TextMeshProUGUI levelLabel;

    [Space]
    [SerializeField] AudioClip levelUpSound;

    [Inject] SignalBus signalBus;

    void Start()
    {
        signalBus.Subscribe<PlayerDamageSignal>(OnPlayerDamage);
        signalBus.Subscribe<PlayerHealSignal>(OnPlayerHeal);

        signalBus.Subscribe<ScoreChangedSignal>(UpdateKillCounter);
        signalBus.Subscribe<PlayerExperienceGainedSignal>(OnPlayerExperienceGained);
        signalBus.Subscribe<PlayerLevelUpSignal>(OnPlayerLevelUp);

        Reset();
    }

    void Reset()
    {
        healthBar.SetValueWithoutNotify(1);
        xpBar.SetValueWithoutNotify(0);
        killCountLabel.text = "0";
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
        killCountLabel.text = signal.Score.ToString();

        killCountLabel.DOKill(false);
        killCountLabel.rectTransform.localScale = Vector3.one;

        // animate label
        killCountLabel.rectTransform.DOPunchScale(Vector3.one * 0.4f, labelsAnimationDuration);
    }

    void OnPlayerExperienceGained(PlayerExperienceGainedSignal signal)
    {
        // dotween sequence to animate xp bar
        var sequence = DOTween.Sequence();

        // fill the xp bar as many times as levels gained
        for (int i = 0; i < signal.LevelsGained; i++)
        {
            sequence.Append(xpBar.DOValue(1, xpAnimationDuration).SetEase(Ease.OutCubic)).OnComplete(() =>
            {
                signalBus.Fire(new PlaySfxSignal(levelUpSound));
            });
            sequence.AppendInterval(0.5f);
            sequence.Append(xpBar.DOValue(0, 0));
        }

        // fill the xp bar to the current progress
        sequence.Append(xpBar.DOValue(signal.Progress, xpAnimationDuration).SetEase(Ease.OutCubic));

        sequence.Play();
    }

    void OnPlayerLevelUp(PlayerLevelUpSignal signal)
    {
        levelLabel.text = levelPrefix + signal.Level.ToString();

        levelLabel.DOKill(false);
        levelLabel.rectTransform.localScale = Vector3.one;

        // animate label
        levelLabel.rectTransform.DOPunchScale(Vector3.one * 0.4f, labelsAnimationDuration);
    }

}
