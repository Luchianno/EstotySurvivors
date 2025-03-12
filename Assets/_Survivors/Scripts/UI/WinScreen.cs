using TMPro;
using UnityEngine;

public class WinScreen : UIScreen
{
    [SerializeField] TextMeshProUGUI scoreText;

    public void Show(int score, bool isNewHighScore)
    {
        scoreText.text = score.ToString();
    }
}
