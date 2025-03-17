using UnityEngine;
using Zenject;

// used in editor/PC to pause the game
public class GamePauser : MonoBehaviour
{
    [Inject] GameStateMachine gameStateMachine;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (gameStateMachine.CurrentState == GameStateMachine.GameState.Playing)
            {
                gameStateMachine.ChangeState(GameStateMachine.GameState.Paused);
            }
            else if (gameStateMachine.CurrentState == GameStateMachine.GameState.Paused)
            {
                gameStateMachine.ChangeState(GameStateMachine.GameState.Playing);
            }
            else
            {
                Debug.Log("Not changing state when it's not playing or paused");
            }
        }
    }
}