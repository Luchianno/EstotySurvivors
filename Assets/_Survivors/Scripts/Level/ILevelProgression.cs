using System.Collections.Generic;
using UnityEngine;

public interface ILevelProgression
{
    int CurrentLevelIndex { get; set; }

    ILevel CurrentLevel { get; }

    List<ILevel> Levels { get; }
}