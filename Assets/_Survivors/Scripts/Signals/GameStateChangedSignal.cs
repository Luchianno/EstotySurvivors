using UnityEngine;

public struct GameStateChangedSignal 
{
    public GameStateMachine.GameState PreviousState { get; }
    public GameStateMachine.GameState NewState { get; }

    public GameStateChangedSignal(GameStateMachine.GameState previousState, GameStateMachine.GameState newState)
    {
        PreviousState = previousState;
        NewState = newState;
    }
}