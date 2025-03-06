using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class EnemyMovementSystem : MonoBehaviour
{
    [Inject] EnemySpawner enemySpawner;

    [Inject(Id = "Player")] Transform player;
    
    void Update()
    {
        UpdateUnits();
    }

    void UpdateUnits()
    {
        var playerPosition = player.position; // caching for efficiency

        // update in batches for future if we have a lot of enemies
        foreach (var enemy in enemySpawner.Enemies)
        {
            if (enemy.Health.IsAlive)
            {
                MoveTowardsTarget(enemy, playerPosition);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void MoveTowardsTarget(EnemyUnit unit, Vector3 target)
    {
        unit.transform.position = Vector3.MoveTowards(unit.transform.position, target, unit.Stats.Speed * Time.deltaTime);
    }

}
