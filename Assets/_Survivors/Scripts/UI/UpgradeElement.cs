using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class UpgradeElement : MonoBehaviour
{
    public UnityEvent<UpgradeData> OnClickEvent = new UnityEvent<UpgradeData>();

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
    [SerializeField] TextMeshProUGUI label;


    void Start()
    {
        button.onClick.AddListener(OpenChest);
    }

    public void Set(UpgradeData upgradeData)
    {
        UpgradeData = upgradeData;

        icon.sprite = upgradeData.Icon;
        label.text = upgradeData.DisplayName;
    }

    void OnEnable()
    {
        chest.sprite = closedChestSprite;

        icon.gameObject.SetActive(false);
        icon.rectTransform.localScale = Vector3.zero;

        button.interactable = true;

        icon.rectTransform.DOScale(Vector3.one, chestAnimationDuration)
            .From(Vector3.zero)
            .SetEase(Ease.OutBounce);
    }

    public void OpenChest()
    {
        button.interactable = false;
        chest.sprite = openChestSprite;
        icon.gameObject.SetActive(true);

        // animate icon
        icon.rectTransform.DOScale(Vector3.one, chestAnimationDuration)
            .From(Vector3.zero)
            .SetEase(Ease.OutBounce);

        OnClickEvent?.Invoke(UpgradeData);
    }
}
