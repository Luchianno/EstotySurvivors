using System;
using UnityEngine;

[Serializable]
public struct EnemyWeightPair: IWeightPair<EnemyData>
{
    [field: SerializeField]
    public EnemyData Data { get; set; }
    [field: SerializeField]
    public float Weight { get; set; }
}