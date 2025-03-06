using UnityEngine;
using Zenject;

// if player doesn't have a target for weapon, flip the player sprite based on input
// if player has a target for weapon, flip the player sprite based on the target position
public class PlayerFlipController : MonoBehaviour
{
    [SerializeField] PlayerWeapon playerWeapon;

    [Inject] IPlayerInput playerInput;

    void Update()
    {
        if (playerWeapon.HasTarget)
        {
            this.transform.localScale = new Vector3(Mathf.Sign(playerWeapon.Target.transform.position.x - this.transform.position.x), 1, 1);
        }
        else
        {
            if (playerInput.MovementInput.x != 0)
            {
                this.transform.localScale = new Vector3(Mathf.Sign(playerInput.MovementInput.x), 1, 1);
            }
        }
    }
}
