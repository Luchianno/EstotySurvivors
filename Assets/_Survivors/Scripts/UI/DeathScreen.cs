using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class DeathScreen : UIScreen
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI countDownText;

    WaitForSeconds wait;

    public void Show(int highScore, bool newHighScore)
    {
        scoreText.text = highScore.ToString();
        countDownText.text = "5";
        wait = new WaitForSeconds(1);
        StartCoroutine(CountDownRoutine());
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
