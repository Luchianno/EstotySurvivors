using System.Collections.Generic;
using UnityEngine;

public class WeightedRandomPicker<T>
{
    float totalWeight;
    List<float> cumulativeWeights = new List<float>();
    List<IWeightPair<T>> pairs;

    public WeightedRandomPicker(IEnumerable<IWeightPair<T>> pairs)
    {
        this.pairs = new List<IWeightPair<T>>(pairs);

        foreach (var pair in pairs)
        {
            totalWeight += pair.Weight;
            cumulativeWeights.Add(totalWeight);
        }
    }

    public T Pick()
    {
        float random = Random.Range(0, totalWeight);

        for (int i = 0; i < pairs.Count; i++)
        {
            if (random <= cumulativeWeights[i])
            {
                return pairs[i].Data;
            }
        }

        return default(T);
    }

}