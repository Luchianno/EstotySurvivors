using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public abstract class UIView : MonoBehaviour
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
        this.gameObject.SetActive(true);

        StartCoroutine(ShowRoutine());
    }

    public virtual void Hide()
    {
        StartCoroutine(HideRoutine());
    }

    protected virtual IEnumerator ShowRoutine()
    {
        canvas.enabled = true;
        mainCanvasGroup.blocksRaycasts = true;

        ToggleBackground(true);
        TogglePanel(true);

        yield return null;
    }

    protected virtual IEnumerator HideRoutine()
    {
        ToggleBackground(false);
        TogglePanel(false);

        yield return new WaitForSeconds(Mathf.Max(backGroundFadeDuration, panelFadeDuration));

        OnHideAnimationEnded();
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