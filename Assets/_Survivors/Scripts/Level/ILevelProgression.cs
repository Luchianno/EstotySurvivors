using System.Collections.Generic;
using UnityEngine;

public interface ILevelProgression
{
    int CurrentLevelIndex { get; set; }

    ILevel CurrentLevel { get; }
    ILevel NextLevel { get; }

    List<ILevel> Levels { get; }
}