using System;
using DG.Tweening;
using TMPro;
using Unity.Android.Gradle;
using UnityEngine;
using Zenject;

public class TextPopup : MonoBehaviour, IPoolable<string, Color, Vector3, IMemoryPool>, IDisposable
{
    [SerializeField] float fadeInTime = 0.2f;
    [SerializeField] float showTime = 0.5f;
    [SerializeField] float fadeOutTime = 0.2f;

    [Space]
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI label;

    IMemoryPool pool;

    public void Dispose()
    {
        pool.Despawn(this);
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void OnSpawned(string text, Color color, Vector3 position, IMemoryPool pool)
    {
        this.pool = pool;

        transform.position = position;
        this.label.text = text;
        this.label.color = color;

        canvasGroup.DOFade(1f, fadeInTime)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                canvasGroup.DOFade(0f, fadeOutTime)
                    .SetDelay(showTime)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        Dispose();
                    });

                transform.DOMoveY(transform.position.y + 1f, showTime)
                    .SetEase(Ease.OutQuad);
            });
    }

    public class Factory : PlaceholderFactory<string, Color, Vector3, TextPopup>
    {
    }
}
