using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
    public Vector2 MovementSpeed;

    Animator animator;

    [Inject] IPlayerInput playerInput;

    public void Tick()
    {
        this.transform.position += new Vector3(playerInput.MovementInput.x * MovementSpeed.x, playerInput.MovementInput.y * MovementSpeed.y, 0);

        if (playerInput.MovementInput.magnitude > 0.1f)
        {
            // animator.SetBool("Speed", 1);
        }
        else
        {
            // animator.SetFloat("Speed", 0);
        }
    }
}
