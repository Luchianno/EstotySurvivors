using UnityEngine;
using Zenject;

public class PlayerFlipController : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] PlayerWeapon playerWeapon;

    [Inject] IPlayerInput playerInput;
    
    void Update()
    {
        // if player doesn't have a target for weapon, flip the player sprite based on input
        // if player has a target for weapon, flip the player sprite based on the target position

        if (playerWeapon.HasTarget)
        {
            if (playerInput.MovementInput.x != 0)
            {
                playerSprite.flipX = playerInput.MovementInput.x < 0;
            }
        }
        else
        {
            playerSprite.flipX = playerWeapon.Target.position.x < transform.position.x;
        }
    }
}
