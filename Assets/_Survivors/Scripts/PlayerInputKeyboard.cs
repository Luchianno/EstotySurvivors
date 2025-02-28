using UnityEngine;

public class PlayerInputKeyboard : IPlayerInput
{
    public Vector2 MovementInput { get; protected set; }

    void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        MovementInput = input;
    }
}
