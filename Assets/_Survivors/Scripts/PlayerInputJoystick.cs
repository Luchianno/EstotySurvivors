using UnityEngine;
using SimpleInputNamespace;

public class PlayerInputJoystick : IPlayerInput
{
    public Vector2 MovementInput { get; protected set; }

    public void Tick()
    {
        // get input from Simple Input System
        var input = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        MovementInput = input;
    }
}
