using UnityEngine;
using Zenject;

public class PlayerInputKeyboard : ITickable, IPlayerInput
{
    public Vector2 MovementInput { get; protected set; }

    public void Tick()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        MovementInput = input;
    }
}
