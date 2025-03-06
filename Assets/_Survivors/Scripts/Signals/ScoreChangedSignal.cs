using UnityEngine;

public struct ScoreChangedSignal 
{
    public int Score { get; }

    public ScoreChangedSignal(int score)
    {
        if(score < 0)
        {
            Debug.LogError("Score cannot be negative");
            score = 0;
        }
        Score = score;
    }

    public override string ToString()
    {
        return $"ScoreChangedSignal: {Score}";
    }

    public static implicit operator int(ScoreChangedSignal signal)
    {
        return signal.Score;
    }

    public static implicit operator ScoreChangedSignal(int score)
    {
        return new ScoreChangedSignal(score);
    }
}
