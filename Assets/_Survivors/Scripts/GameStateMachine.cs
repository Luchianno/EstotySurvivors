using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStateMachine : MonoBehaviour
{
    public bool IsPlaying => CurrentGameState == GameState.Playing;
    public GameState CurrentGameState { get; protected set; }

    [Inject] SignalBus signalBus;

    // UI Screens
    [Inject] LandingScreen landingScreen;
    [Inject(Id = "Lose")] EndgameScreen loseScreen;
    [Inject(Id = "Win")] EndgameScreen winScreen;

    // gameplay systems in need of enabling/disabling based on game state
    [Inject(Id = "Player")] Transform player;
    [Inject] EnemySpawner enemySpawner;
    [Inject] PropSpawner propSpawner;
    [Inject] EnemyMovementSystem enemyMovementSystem;

    // other
    [Inject] PlayerStatsManager statsManager;
    [Inject] AudioManager audioManager;

    void Start()
    {
        ChangeState(GameState.Landing);

        signalBus.Subscribe<PlayerDeathSignal>(OnPlayerDeath);

        void OnPlayerDeath(PlayerDeathSignal signal)
        {
            ChangeState(GameState.DeathScreen);
        }
    }

    public void ChangeState(GameState gameState)
    {
        StartCoroutine(ChangeStateRoutine(gameState));
    }

    IEnumerator ChangeStateRoutine(GameState gameState)
    {
        CurrentGameState = gameState;

        switch (CurrentGameState)
        {
            case GameState.Landing:
                player.gameObject.SetActive(false);
                enemySpawner.enabled = false;
                propSpawner.enabled = false;

                // activate landing screen
                landingScreen.Show();
                yield return new WaitForSeconds(3f);
                landingScreen.Hide();
                yield return new WaitForSeconds(0.3f);

                ChangeState(GameState.Playing);

                break;
            case GameState.Playing:
                player.gameObject.SetActive(true);

                enemySpawner.enabled = true;
                propSpawner.enabled = true;

                break;
            case GameState.DeathScreen:
                audioManager.FadeMusicVolume(0.1f, 0.3f);
                loseScreen.Show(statsManager.CurrentScore, statsManager.IsNewHighScore);

                yield return new WaitForSeconds(5.5f);
                
                statsManager.ResetAndSaveHighScore();

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case GameState.WinScreen:
                audioManager.FadeMusicVolume(0.1f, 0.3f);
                winScreen.Show();

                yield return new WaitForSeconds(5.5f);
                
                statsManager.ResetAndSaveHighScore();

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }

        yield return null;
    }


    public enum GameState
    {
        Landing,
        Playing,
        DeathScreen,
        WinScreen,
    }
}
