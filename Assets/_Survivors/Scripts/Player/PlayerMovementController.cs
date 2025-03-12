using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
    public UpdateType UpdateWhen = UpdateType.Update;
    public float MovementSpeed = 1f;
    public float RunningAnimationThreshold = 0.1f;

    [SerializeField] Animator animator;

    [Inject] IPlayerInput playerInput;

    void Update()
    {
        if (UpdateWhen == UpdateType.Update)
            Tick();
    }

    public void Tick()
    {
        this.transform.position += new Vector3(playerInput.MovementInput.x, playerInput.MovementInput.y, 0) * (MovementSpeed * Time.deltaTime);

        animator.SetBool("Running", playerInput.MovementInput.sqrMagnitude >= RunningAnimationThreshold * RunningAnimationThreshold);
    }

    public enum UpdateType
    {
        Manual,
        Update,
    }
}
