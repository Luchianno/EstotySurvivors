using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

// Spawn enemies individually and by groups as well
public class EnemySpawner : BasePausableBehaviour
{
    public UnityEvent<EnemyUnit> OnEnemySpawnedEvent;
    public UnityEvent<EnemyUnit> EnemyDeathEvent;

    public HashSet<EnemyUnit> Enemies => enemies;

    [SerializeField] int groupSpawnMinAmount = 3;
    [SerializeField] int groupSpawnMaxAmount = 5;

    HashSet<EnemyUnit> enemies = new HashSet<EnemyUnit>();

    [Inject] LevelProgression levelProgression;
    [Inject] SpawningArea spawningArea;
    [Inject] EnemyUnit.Pool enemyFactory;

    // cache WaitForSeconds to avoid creating extra garbage
    WaitForSeconds waitASecond = new WaitForSeconds(1f);

    [Inject] SignalBus signalBus;

    void Start()
    {
        StartCoroutine(UpdateRoutine());

        signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
    }

    public bool ShouldSpawnMoreEnemies()
    {
        return enemies.Count < levelProgression.CurrentLevel.MaxEnemies;
    }

    public void SpawnIndividualEnemy() => SpawnIndividualEnemy(levelProgression.CurrentLevel.GetRandomEnemyType(), spawningArea.GetRandomPosition());

    public void SpawnIndividualEnemy(EnemyData enemyData, Vector3 position)
    {
        var unit = enemyFactory.Create(position, enemyData);

        enemies.Add(unit);

        signalBus.Fire(new EnemySpawnedSignal(unit));
        OnEnemySpawnedEvent.Invoke(unit);
    }

    public void SpawnEnemyGroup(int minAmount, int maxAmount) => SpawnEnemyGroup(Random.Range(minAmount, maxAmount));

    public void SpawnEnemyGroup(int amount)
    {
        var enemyType = levelProgression.CurrentLevel.GetRandomEnemyType();

        for (int i = 0; i < amount; i++)
        {
            var positionCenter = spawningArea.GetRandomPosition();
            var randomDirection2D = Random.insideUnitCircle.normalized;
            var positionWithNoise = positionCenter + randomDirection2D;
            var enemy = enemyFactory.Create(positionWithNoise, enemyType);

            enemies.Add(enemy);
        }
    }


    void OnEnemyDeath(EnemyDeathSignal signal)
    {
        enemies.Remove(signal.EnemyUnit);

        EnemyDeathEvent.Invoke(signal.EnemyUnit);
    }

    IEnumerator UpdateRoutine()
    {
        while (true)
        {
            // check if we should spawn more enemies
            if (ShouldSpawnMoreEnemies())
            {
                // lets toss a coin again
                if (Random.value > 0.5f)
                {
                    SpawnIndividualEnemy();
                }
                else
                {
                    // now lets roll a dice to decide how many enemies to spawn
                    SpawnEnemyGroup(groupSpawnMinAmount, groupSpawnMaxAmount);
                }
            }

            yield return waitASecond;
        }

    }

}
