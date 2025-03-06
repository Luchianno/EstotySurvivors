using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUDView : MonoBehaviour
{
    [SerializeField] Color healingColor;
    [SerializeField] Image healthBar;
    [SerializeField] Image xpBar;
    [SerializeField] TextMeshProUGUI killCountText;

    [Inject] SignalBus signalBus;

    private void Start()
    {
        signalBus.Subscribe<PlayerDamageSignal>(OnPlayerDamage);
        signalBus.Subscribe<PlayerHealSignal>(OnPlayerHeal);

        signalBus.Subscribe<ScoreChangedSignal>(UpdateKillCounter);

        Reset();
    }

    void Reset()
    {
        healthBar.fillAmount = 1;
        xpBar.fillAmount = 0;
        killCountText.text = "0";
    }


    void OnPlayerDamage(PlayerDamageSignal signal)
    {
        healthBar.DOFillAmount((float)signal.Amount / 100, 0.3f);
    }

    void OnPlayerHeal(PlayerHealSignal signal)
    {
        healthBar.DOFillAmount((float)signal.Current / signal.Max, 0.3f);
    }

    void UpdateKillCounter(ScoreChangedSignal signal)
    {
        killCountText.text = signal.Score.ToString();

        // animate label
        killCountText.transform.DOPunchScale(Vector3.one * 0.4f, 0.5f);
    }

}
