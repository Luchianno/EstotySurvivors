using UnityEngine;

public struct EndGameSignal 
{
    public bool IsWin { get; }
    public int Score { get; }
    public bool IsNewHighScore { get; }

    public EndGameSignal(bool isWin, int score, bool isNewHighScore)
    {
        IsWin = isWin;
        Score = score;
        IsNewHighScore = isNewHighScore;
    }

}
