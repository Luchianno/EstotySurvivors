using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TauntList", menuName = "💀 Survivors/Taunt List")]
public class TauntList : ScriptableObject
{
    [SerializeField] List<string> taunts;

    public string GetRandomTaunt()
    {
        if (taunts.Count == 0)
        {
            Debug.LogError("No taunts found in the list");
            return null;
        }

        return taunts[Random.Range(0, taunts.Count)];
    }
}
