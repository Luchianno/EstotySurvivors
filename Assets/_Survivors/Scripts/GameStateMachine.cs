using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStateMachine : MonoBehaviour
{
    public bool IsPlaying => CurrentState == GameState.Playing;
    public GameState CurrentState { get; protected set; } = GameState.None;

    [Inject] SignalBus signalBus;

    // gameplay systems in need of enabling/disabling based on game state
    [Inject] List<IPausable> pausableGameSystems;
    [Inject] PlayerStatsManager statsManager;
    [Inject] AudioManager audioManager;
    [Inject] LevelProgression levelProgression;

    void Start()
    {
        ChangeState(GameState.Landing);

        signalBus.Subscribe<PlayerDeathSignal>(OnPlayerDeath);
        signalBus.Subscribe<PlayerLevelUpSignal>(OnPlayerLevelUp);
        signalBus.Subscribe<UpgradeSelectedSignal>(OnUpgradeSelected);

        void OnPlayerDeath(PlayerDeathSignal signal)
        {
            ChangeState(GameState.EndgameLose);
        }

        void OnPlayerLevelUp(PlayerLevelUpSignal signal)
        {
            // if it's a last level up, we go to endgame win state
            ChangeState(levelProgression.IsMaxLevel ? GameState.EndgameWin : GameState.LevelUp);
        }

        void OnUpgradeSelected(UpgradeSelectedSignal signal)
        {
            // if we are in level up state, we can go back to playing
            if (CurrentState == GameState.LevelUp)
            {
                ChangeState(GameState.Playing);
            }
        }
    }

    public void ChangeState(GameState gameState)
    {
        if (CurrentState == gameState)
            return;

        ChangeStateRoutine(gameState).Forget();
    }

    async UniTaskVoid ChangeStateRoutine(GameState gameState)
    {
        var previousState = CurrentState;

        CurrentState = gameState;

        signalBus.Fire(new GameStateChangedSignal(previousState, CurrentState));

        switch (CurrentState)
        {
            case GameState.Landing:
                ToggleGamePaused(true);

                // activate landing screen
                signalBus.Fire(new UIViewSignal(UISignalType.Show, "Landing"));
                await UniTask.WaitForSeconds(3f);
                signalBus.Fire(new UIViewSignal(UISignalType.Hide, "Landing"));
                await UniTask.WaitForSeconds(0.3f);

                ChangeState(GameState.Playing);
                break;

            case GameState.Paused:
                ToggleGamePaused(true);
                break;

            case GameState.Playing:
                ToggleGamePaused(false);
                break;

            case GameState.LevelUp:
                ToggleGamePaused(true);

                signalBus.Fire(new UIViewSignal(UISignalType.Show, "LevelUp"));

                break;
            case GameState.EndgameLose:
            case GameState.EndgameWin:
                ToggleGamePaused(true);

                var isWin = CurrentState == GameState.EndgameWin;
                var viewName = isWin ? "Win" : "Lose";

                // fade out music
                audioManager.FadeMusicVolume(0.1f, 0.3f);

                // update UI with score and highscore and show lose screen
                signalBus.Fire(new EndGameSignal(isWin, statsManager.CurrentScore, statsManager.IsNewHighScore));
                signalBus.Fire(new UIViewSignal(UISignalType.Show, viewName));

                // don't forget to reset and save  highscore
                statsManager.ResetAndSaveHighScore();

                // wait for a bit before restarting the game
                await UniTask.WaitForSeconds(5.5f);

                // reload this scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                Debug.LogError($"Unhandled game state: {CurrentState}");
                break;
        }
    }

    void ToggleGamePaused(bool isPaused)
    {
        foreach (var item in pausableGameSystems)
        {
            item.SetPaused(isPaused);
        }
    }

    public enum GameState
    {
        None,
        Landing,
        Paused,
        Playing,
        LevelUp,
        EndgameLose,
        EndgameWin,
    }
}
