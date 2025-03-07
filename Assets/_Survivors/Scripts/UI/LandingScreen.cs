using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LandingScreen : UIScreen
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

    public override void Show()
    {   
        base.Show();
        
        var titleRect = (RectTransform)title.transform;

        // move title from top to current position
        titleRect.DOAnchorPosY(titleRect.anchoredPosition.y, 1f)
            .From(new Vector3(0, 1000, 0))
            .SetEase(Ease.OutBounce);

        chaseParent.DOAnchorPosY(chaseParent.anchoredPosition.y, 1f)
            .From(new Vector3(0, -1000, 0))
            .SetEase(Ease.OutBack);

        // move zombie from left to right and back again
        zombieImage.DOAnchorPosX(2, 1.1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // move survivor from right to left and back again
        survivorImage.DOAnchorPosX(-2, 1.3f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
        
    }

    public override void Hide()
    {
        base.Hide();
        var titleRect = (RectTransform)title.transform;

        // move title from current position to top
        titleRect.DOLocalMoveY(1000, 1f)
            .SetEase(Ease.InBounce);


        // zombie goes to left 
        zombieImage.DOAnchorPosX(-10, 1.1f)
            .SetEase(Ease.InOutSine);

        // survivor goes to right
        survivorImage.DOAnchorPosX(10, 1.3f)
            .SetEase(Ease.InOutSine);

    }
}
