using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HighScoreManager : MonoBehaviour
{

    public int HighScore { get; protected set; }

    public Dictionary<EnemyData, int> EnemyKills { get; protected set; } = new Dictionary<EnemyData, int>();

    [Inject] SignalBus signalBus;

    void OnEnable()
    {
        Reset();

        signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
    }

    void OnDisable()
    {
        signalBus.Unsubscribe<EnemyDeathSignal>(OnEnemyDeath);
    }

    public void Reset()
    {
        HighScore = 0;
        EnemyKills.Clear();
    }

    void OnEnemyDeath(EnemyDeathSignal signal)
    {
        HighScore++;

        var enemyType = signal.EnemyUnit.Data;

        if (EnemyKills.ContainsKey(enemyType))
            EnemyKills[enemyType]++;
        else
            EnemyKills.Add(enemyType, 1);
        
        signalBus.Fire(new ScoreChangedSignal(HighScore));
    }

}
