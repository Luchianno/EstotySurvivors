using UnityEngine;
using Zenject;

public interface IPlayerInput: ITickable
{
    Vector2 MovementInput { get; }
}
