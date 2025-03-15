using UnityEngine;
using Zenject;

public class PlayerMovementController : BasePausableBehaviour
{
    public UpdateType UpdateWhen = UpdateType.Update;
    public float MovementSpeed = 1f;
    public float RunningAnimationThreshold = 0.1f;

    [SerializeField] Animator animator;
    [SerializeField] PlayerHealth playerHealth;

    [Inject] IPlayerInput playerInput;

    void Update()
    {
        if(!playerHealth.IsAlive)
        {
            animator.SetBool("Running", false);
            return;
        }

        if (UpdateWhen == UpdateType.Update)
            Tick();
    }

    public void Tick()
    {
        this.transform.position += new Vector3(playerInput.MovementInput.x, playerInput.MovementInput.y, 0) * (MovementSpeed * Time.deltaTime);

        animator.SetBool("Running", playerInput.MovementInput.sqrMagnitude >= RunningAnimationThreshold * RunningAnimationThreshold);
    }

    // In case we change movement to be conrtrolled by FixedUpdate or manually through other means
    public enum UpdateType
    {
        Manual,
        Update,
    }
}
