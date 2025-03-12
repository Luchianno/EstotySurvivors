using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerStatsManager : IInitializable
{
    public const string PrefsPath = "HighScore";

    public int CurrentScore { get; protected set; }
    public int AlltimeHighScore { get; protected set; }
    public bool IsNewHighScore => CurrentScore > AlltimeHighScore;

    public Dictionary<EnemyData, int> EnemyKills { get; protected set; } = new Dictionary<EnemyData, int>();

    [Inject] SignalBus signalBus;

    public void Initialize()
    {
        signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);

        AlltimeHighScore = GetHighScore();
    }

    public void ResetAndSaveHighScore()
    {
        if (IsNewHighScore)
        {
            AlltimeHighScore = CurrentScore;
            SaveHighScore(AlltimeHighScore);
        }

        CurrentScore = 0;
        EnemyKills.Clear();
    }

    // TODO to be changed to be synced with cloud
    protected virtual int GetHighScore(int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(PrefsPath, defaultValue);
    }

    protected virtual void SaveHighScore(int value)
    {
        PlayerPrefs.SetInt(PrefsPath, value);
        PlayerPrefs.Save();
    }


    void OnEnemyDeath(EnemyDeathSignal signal)
    {
        CurrentScore++;

        var enemyType = signal.EnemyUnit.Data;

        if (EnemyKills.ContainsKey(enemyType))
            EnemyKills[enemyType]++;
        else
            EnemyKills.Add(enemyType, 1);

        signalBus.Fire(new ScoreChangedSignal(CurrentScore));
    }

}
