using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] Image chest;

    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closedChestSprite;

    [SerializeField] AudioClip selectedSound;

    [SerializeField] float chestAnimationDuration = 1f;

    [Inject] SignalBus signalBus;

    void Start()
    {
        button.onClick.AddListener(OpenChest);
    }

    void Reset()
    {
        chest.sprite = closedChestSprite;
        icon.rectTransform.localScale = Vector3.zero;
        button.interactable = true;
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
