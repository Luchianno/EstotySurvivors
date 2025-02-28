using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
    public UpdateType UpdateWhen = UpdateType.Update;
    public float MovementSpeed = 1f;
    public float RunningAnimationThreshold = 0.1f;

    [SerializeField] Animator animator;

    [Inject] IPlayerInput playerInput;

    public void Tick()
    {
        this.transform.position += new Vector3(playerInput.MovementInput.x, playerInput.MovementInput.y, 0) * (MovementSpeed * Time.deltaTime);

        animator.SetBool("IsRunning", playerInput.MovementInput.sqrMagnitude >= RunningAnimationThreshold * RunningAnimationThreshold);

        if(playerInput.MovementInput.x != 0)
        {
            this.transform.localScale = new Vector3(Mathf.Sign(playerInput.MovementInput.x), 1, 1);
        }
    }

    void Update()
    {
        if (UpdateWhen == UpdateType.Update)
            Tick();
    }

    public enum UpdateType
    {
        Manual,
        Update,
    }
}
