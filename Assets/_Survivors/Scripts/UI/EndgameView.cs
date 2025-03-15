using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class EndgameView : UIView
{
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI countDownText;

    [Space]
    [SerializeField] string scoreText = "Zombies Killed: ";
    [SerializeField] string highScoreText = "NEW HIGH SCORE: ";
    [SerializeField] Color newHighScoreColor = Color.red;
    [SerializeField] float highsScoreFlashPeriod = 0.5f;
    [SerializeField] AudioClip soundEffect;

    [Inject] SignalBus signalBus;
    
    WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);

    protected override void Awake()
    {
        base.Awake();
        signalBus.Subscribe<EndGameSignal>(OnEndgameSignal);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        signalBus.TryUnsubscribe<EndGameSignal>(OnEndgameSignal);
    }

    void OnEndgameSignal(EndGameSignal signal)
    {
        scoreLabel.text = signal.IsNewHighScore ? highScoreText + signal.Score : scoreText + signal.Score;
        countDownText.text = "5";

        scoreLabel.DOKill();
        scoreLabel.rectTransform.DOKill();

        if (signal.IsNewHighScore)
        {
            // animate new high score text
            scoreLabel.DOColor(newHighScoreColor, highsScoreFlashPeriod)
                .SetUpdate(true)
                .SetEase(Ease.InOutCirc)
                .SetLoops(-1, LoopType.Yoyo);
            
            scoreLabel.rectTransform.DOScale(1.1f, highsScoreFlashPeriod)
                .SetUpdate(true)
                .SetEase(Ease.InOutCirc)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    public override void Show()
    {
        base.Show();

        StartCoroutine(CountDownRoutine());
        signalBus.Fire(new PlaySfxSignal(soundEffect));
    }

    IEnumerator CountDownRoutine()
    {
        int countDown = 5;
        while (countDown > 0)
        {
            countDownText.text = countDown.ToString();
            yield return wait;
            countDown--;
        }

    }
}
