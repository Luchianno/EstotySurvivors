using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStateMachine : MonoBehaviour
{
    public bool IsPlaying => CurrentGameState == GameState.Playing;
    public GameState CurrentGameState { get; protected set; }

    [Inject] SignalBus signalBus;

    // gameplay systems in need of enabling/disabling based on game state
    [Inject] List<IPausable> pausableGameSystems = new List<IPausable>();

    // other
    [Inject] PlayerStatsManager statsManager;
    [Inject] AudioManager audioManager;

    void Start()
    {
        ChangeState(GameState.Landing);

        signalBus.Subscribe<PlayerDeathSignal>(OnPlayerDeath);

        void OnPlayerDeath(PlayerDeathSignal signal)
        {
            ChangeState(GameState.EndgameLose);
        }
    }

    public void ChangeState(GameState gameState)
    {
        if (CurrentGameState == gameState)
            return;

        ChangeStateRoutine(gameState).Forget();
    }

    void SetPaused(bool isPaused)
    {
        foreach (var item in pausableGameSystems)
        {
            item.SetPaused(isPaused);
        }
    }

    async UniTaskVoid ChangeStateRoutine(GameState gameState)
    {
        var previousState = CurrentGameState;

        CurrentGameState = gameState;

        signalBus.Fire(new GameStateChangedSignal(previousState, CurrentGameState));

        switch (CurrentGameState)
        {
            case GameState.Landing:
                SetPaused(true);

                // activate landing screen
                signalBus.Fire(new UIViewSignal(UISignalType.Show, "Landing"));
                await UniTask.WaitForSeconds(0.3f);
                signalBus.Fire(new UIViewSignal(UISignalType.Hide, "Landing"));
                await UniTask.WaitForSeconds(0.3f);

                ChangeState(GameState.Playing);

                break;

            case GameState.Paused:
                SetPaused(true);

                // TODO activate pause screen

                break;
            case GameState.Playing:
                SetPaused(false);

                break;
            case GameState.LevelUp:
                Time.timeScale = 0f;

                signalBus.Fire(new UIViewSignal(UISignalType.Show, "LevelUp"));

                break;
            case GameState.EndgameLose:
            case GameState.EndgameWin:
                SetPaused(true);

                var isWin = CurrentGameState == GameState.EndgameWin;
                var viewName = isWin ? "Win" : "Lose";

                // fade out music
                audioManager.FadeMusicVolume(0.1f, 0.3f);

                // update UI with score and highscore and show lose screen
                signalBus.Fire(new EndGameSignal(isWin, statsManager.CurrentScore, statsManager.IsNewHighScore));
                signalBus.Fire(new UIViewSignal(UISignalType.Show, viewName));

                // wait for a bit before restarting the game
                await UniTask.WaitForSeconds(5.5f);

                // don't forget to reset and save  highscore
                statsManager.ResetAndSaveHighScore();
                break;
            default:
                Debug.LogError($"Unhandled game state: {CurrentGameState}");
                break;
        }
    }



    public enum GameState
    {
        Landing,
        Paused,
        Playing,
        LevelUp,
        EndgameLose,
        EndgameWin,
    }
}
