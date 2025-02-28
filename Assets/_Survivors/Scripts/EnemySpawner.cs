using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

// Spawn enemies individually and by waves as well
public class EnemySpawner : MonoBehaviour
{
    List<EnemyUnit> enemies = new List<EnemyUnit>();

    [SerializeField] List<WeightPair> enemyTypes;

    // enemy object pools:
    [Inject] EnemySpawningArea spawningArea;
    [Inject] EnemyUnit.Pool enemyPool;

    // cache WaitForSeconds to avoid creating extra garbage
    WaitForSeconds waitASecond = new WaitForSeconds(1f);
    Coroutine updateRoutine;
    List<float> cumulativeWeights = new List<float>();

    void Awake()
    {
        float totalWeight = 0;
        foreach (var pair in enemyTypes)
        {
            totalWeight += pair.Weight;
            cumulativeWeights.Add(totalWeight);
        }
    }

    void OnEnable()
    {
        updateRoutine = StartCoroutine(UpdateRoutine());
    }

    void OnDisable()
    {
        StopCoroutine(updateRoutine);
    }


    public bool ShouldSpawnMoreEnemies()
    {
        return enemies.Count < 10;
    }

    public void SpawnIndividualEnemy()
    {
        var selectedEnemy = GetRandomEnemyType();

        var positionCenter = spawningArea.GetRandomPosition();
        var unit = enemyPool.Spawn(positionCenter, selectedEnemy);

        enemies.Add(unit);
    }

    public void SpawnEnemyGroup(int minAmount, int maxAmount) => SpawnEnemyGroup(Random.Range(minAmount, maxAmount));

    public void SpawnEnemyGroup(int amount)
    {
        var enemyType = GetRandomEnemyType();

        for (int i = 0; i < amount; i++)
        {
            var positionCenter = spawningArea.GetRandomPosition();
            var randomDirection2D = Random.insideUnitCircle.normalized;
            var positionWithNoise = positionCenter + randomDirection2D;
            var enemy = enemyPool.Spawn(positionWithNoise, enemyType);

            enemies.Add(enemy);
        }
    }

    EnemyData GetRandomEnemyType()
    {
        float totalWeight = enemyTypes.Sum(x => x.Weight);
        float randomValue = Random.value * totalWeight;

        int selectedIndex = cumulativeWeights.BinarySearch(randomValue);
        if (selectedIndex < 0)
        {
            selectedIndex = ~selectedIndex;
        }

        return enemyTypes[selectedIndex].Data;
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
                    SpawnEnemyGroup(Random.Range(3, 5));
                }
            }

            yield return waitASecond;
        }

    }

    [Serializable]
    public struct WeightPair
    {
        public EnemyData Data;
        public float Weight;
    }
}
