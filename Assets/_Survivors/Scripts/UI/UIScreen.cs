using DG.Tweening;
using log4net.Util;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected CanvasGroup mainCanvasGroup;
    [SerializeField] protected CanvasGroup background;
    [SerializeField] protected CanvasGroup panel;

    [SerializeField] float backGroundFadeDuration = 0.5f;
    [SerializeField] float panelFadeDuration = 0.5f;

    public bool DisableGameObjectOnHide;

    public virtual void Show()
    {
        gameObject.SetActive(true);

        canvas.enabled = true;
        mainCanvasGroup.blocksRaycasts = true;

        ToggleBackground(true);
        TogglePanel(true);
    }

    public virtual void Hide()
    {
        ToggleBackground(false);
        TogglePanel(false);
    }

    protected virtual void OnHideAnimationEnded()
    {
        canvas.enabled = false;
        mainCanvasGroup.blocksRaycasts = false;

        if (DisableGameObjectOnHide)
            gameObject.SetActive(false);

    }

    protected virtual void ToggleBackground(bool show)
    {
        if (background == null)
            return;

        if (show)
        {
            background.alpha = 0;
            background.gameObject.SetActive(true);
        }

        background.DOFade(show ? 1 : 0, backGroundFadeDuration)
            .SetEase(show ? Ease.OutSine : Ease.InSine);
    }

    protected virtual void TogglePanel(bool show)
    {
        if (panel == null)
            return;

        if (show)
        {
            panel.alpha = 0;
            panel.gameObject.SetActive(true);
        }

        panel.DOFade(show ? 1 : 0, panelFadeDuration)
            .SetEase(show ? Ease.OutSine : Ease.InSine);
    }
}