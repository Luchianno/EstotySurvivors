using System;
using UnityEngine;

[Serializable]
public struct PropWeightPair : IWeightPair<PropData>
{
    [field: SerializeField]
    public PropData Data { get; set; }
    [field: SerializeField]
    public float Weight { get; set; }
}
