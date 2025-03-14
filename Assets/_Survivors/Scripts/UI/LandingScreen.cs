using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LandingScreen : UIView
{
    [Space]
    [SerializeField] Image title;
    [SerializeField] RectTransform chaseParent;
    [SerializeField] RectTransform zombieImage;
    [SerializeField] RectTransform survivorImage;

    void Start()
    {
        Show();
    }

    protected override IEnumerator ShowRoutine()
    {
        var titleRect = (RectTransform)title.transform;

        // move title from top to current position
        titleRect.DOAnchorPosY(80, 1f) // Animate to the original Y position
            .From(new Vector2(titleRect.anchoredPosition.y, 120)) // Start from Y position 1000
            .SetEase(Ease.OutBounce);

        // chaseParent.DOAnchorPosY(-90f, 1f)
        //     .From(new Vector3(0, -200, 0))
        //     .SetEase(Ease.OutCubic);

        // move zombie from left to right and back again
        zombieImage.DOAnchorPosX(0, 1.1f)
            .SetDelay(0.2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // move survivor from right to left and back again
        survivorImage.DOAnchorPosX(0, 1.3f)
            .SetDelay(0.3f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        yield return null;
    }

    protected override IEnumerator HideRoutine()
    {
        ToggleBackground(false);
        TogglePanel(false);

        var titleRect = (RectTransform)title.transform;

        // move title from current position to top
        titleRect.DOAnchorPosY(200, 1f)
            .SetEase(Ease.InCubic)
            .OnComplete(() => OnHideAnimationEnded());


        zombieImage.DOKill();
        survivorImage.DOKill();

        chaseParent.DOAnchorPosY(-250, 1f)
            .SetEase(Ease.InCubic);

        yield return new WaitForSeconds(2f);

        OnHideAnimationEnded();
    }
}
