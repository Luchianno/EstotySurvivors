using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class DeathScreen : UIScreen
{
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI countDownText;

    [Space]
    [SerializeField] string scoreText = "Zombies Killed: ";
    [SerializeField] string highScoreText = "NEW HIGH SCORE: ";
    [SerializeField] Color newHighScoreColor = Color.red;
    [SerializeField] float highsScoreFlashPeriod = 0.5f;
    
    WaitForSeconds wait = new WaitForSeconds(1);

    public void Show(int highScore, bool newHighScore)
    {
        gameObject.SetActive(true);

        scoreLabel.text = newHighScore ? highScoreText + highScore : scoreText + highScore;
        countDownText.text = "5";

        StartCoroutine(CountDownRoutine());

        scoreLabel.DOKill();
        scoreLabel.rectTransform.DOKill();

        if (newHighScore)
        {
            // animate new high score text
            scoreLabel.DOColor(newHighScoreColor, highsScoreFlashPeriod)
                .SetEase(Ease.InOutCirc)
                .SetLoops(-1, LoopType.Yoyo);
            
            scoreLabel.rectTransform.DOScale(1.1f, highsScoreFlashPeriod)
                .SetEase(Ease.InOutCirc)
                .SetLoops(-1, LoopType.Yoyo);
        }

        base.Show();
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
