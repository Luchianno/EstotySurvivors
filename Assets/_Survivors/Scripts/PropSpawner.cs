using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PropSpawner : MonoBehaviour
{
    [Header("Once per second, this chance is checked and then weighted random prop is spawned")]
    [SerializeField] float chancePerSecond = 0.1f;
    [SerializeField] float tickRateSeconds = 1f;
    [SerializeField] List<PropWeightPair> props;

    [Inject] SignalBus signalBus;
    [Inject] SpawningArea spawningArea; // we're using same area as where enemies spawn
    [Inject] PropItem.Factory propFactory;

    WeightedRandomPicker<PropData> propPicker;
    // cache WaitForSeconds to avoid creating extra garbage
    WaitForSeconds wait;
    Coroutine updateRoutine;

    void Awake()
    {
        propPicker = new WeightedRandomPicker<PropData>(props.Cast<IWeightPair<PropData>>());
        wait = new WaitForSeconds(tickRateSeconds);
    }

    void OnEnable()
    {
        updateRoutine = StartCoroutine(UpdateRoutine());
    }

    void OnDisable()
    {
        StopCoroutine(updateRoutine);
    }

    IEnumerator UpdateRoutine()
    {
        while (true)
        {
            yield return wait;

            if (Random.value < chancePerSecond)
            {
                SpawnProp(propPicker.Pick());
            }
        }
        
    }

    void SpawnProp(PropData data)
    {
        var prop = propFactory.Create(spawningArea.GetRandomPosition(), data);
    }

    
}
