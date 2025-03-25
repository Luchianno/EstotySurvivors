using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public abstract class UIView : MonoBehaviour
{
    public string ViewName;
    public StartupType Startup = StartupType.DisableGameObject;
    public bool IsShowing => gameObject.activeSelf && canvas.enabled;

    [SerializeField] protected Canvas canvas;
    [SerializeField] protected CanvasGroup mainCanvasGroup;
    [Space]
    [SerializeField] protected CanvasGroup background;
    [SerializeField] protected CanvasGroup panel;

    [SerializeField] float backGroundFadeDuration = 0.5f;
    [SerializeField] float panelFadeDuration = 0.5f;

    public bool DisableGameObjectOnHide;

    [Inject] SignalBus signalBus;

    protected virtual void Awake()
    {
        signalBus.Subscribe<UIViewSignal>(OnUIViewSignal);

        switch (Startup)
        {
            case StartupType.DisableGameObject:
                gameObject.SetActive(false);
                break;
            case StartupType.Show:
                Show();
                break;
            case StartupType.Hide:
                Hide();
                break;
        }
    }

    protected virtual void OnUIViewSignal(UIViewSignal signal)
    {
        if (signal.ViewName == ViewName)
        {
            switch (signal.SignalType)
            {
                case UISignalType.Show:
                    if (IsShowing)
                        return;
                    Show();
                    break;
                case UISignalType.Hide:
                    if (!IsShowing)
                        return;
                    Hide();
                    break;
                case UISignalType.Toggle:
                    if (IsShowing)
                        Hide();
                    else
                        Show();
                    break;
            }
        }
    }

    // we're not using Views that are going to be destroyed, but just in case
    protected virtual void OnDestroy()
    {
        signalBus.TryUnsubscribe<UIViewSignal>(OnUIViewSignal);
    }

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

        yield return new WaitForSecondsRealtime(Mathf.Max(backGroundFadeDuration, panelFadeDuration));

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
            .SetUpdate(true)
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
        else
        {
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }

        panel.DOFade(show ? 1 : 0, panelFadeDuration)
            .SetUpdate(true)
            .SetEase(show ? Ease.OutSine : Ease.InSine)
            .OnComplete(() =>
            {
                if (show)
                {
                    panel.interactable = true;
                    panel.blocksRaycasts = true;
                }
            });
    }

    public enum StartupType
    {
        DisableGameObject,
        Show,
        Hide
    }
}