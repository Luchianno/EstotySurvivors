using DG.Tweening;
using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    [SerializeField] private float flashInterval = 0.5f;
    [SerializeField] private Color flashColor = Color.green;
    [SerializeField] TextMeshProUGUI label;

    void OnEnable()
    {
        label.DOColor(flashColor, flashInterval).SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        label.DOKill();
    }

}