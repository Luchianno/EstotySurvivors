using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeElement : MonoBehaviour
{
    [field: SerializeField]
    public UpgradeData UpgradeData { get; protected set; }

    [Space]
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] Image chest;

    [Space]
    [SerializeField] float chestAnimationDuration = 1f;
    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closedChestSprite;
    [SerializeField] AudioClip selectedSound;

    [Inject] SignalBus signalBus;

    void Start()
    {
        button.onClick.AddListener(OpenChest);
    }

    public void Set(UpgradeData upgradeData)
    {
        UpgradeData = upgradeData;

        icon.sprite = upgradeData.Icon;
        chest.sprite = closedChestSprite;
        
        icon.rectTransform.localScale = Vector3.zero;
        button.interactable = true;
    }

    void OnEnable()
    {
        icon.rectTransform.DOScale(Vector3.one, chestAnimationDuration)
            .From(Vector3.zero)
            .SetEase(Ease.OutBounce);
    }

    public void OpenChest()
    {
        button.interactable = false;
        chest.sprite = openChestSprite;

        // animate icon
        icon.rectTransform.DOScale(Vector3.one, chestAnimationDuration)
            .From(Vector3.zero)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => icon.rectTransform.DOScale(Vector3.zero, chestAnimationDuration).SetEase(Ease.InBounce)
            );

        // play sfx
        signalBus.Fire(new PlaySfxSignal(selectedSound));
    }
}
