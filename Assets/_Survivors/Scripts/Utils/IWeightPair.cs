using System;
using UnityEngine;

public interface IWeightPair<T>
{
    T Data { get; set; }
    float Weight { get; set; }
}
