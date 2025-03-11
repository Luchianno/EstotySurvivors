using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    public bool IsPlaying => CurrentGameState == GameState.Playing;
    public GameState CurrentGameState { get; protected set; }

    [Inject] SignalBus signalBus;
    [Inject] LandingScreen landingScreen;
    [Inject] DeathScreen deathScreen;
    [Inject] WinScreen winScreen;

    [Inject(Id = "Player")] Transform player;
    [Inject] EnemySpawner enemySpawner;
    [Inject] PropSpawner propSpawner;
    [Inject] EnemyMovementSystem  enemyMovementSystem;

    void Start()
    {
        ChangeState(GameState.Landing);
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
                yield return new WaitForSeconds(5.5f);
                landingScreen.Hide();
                yield return new WaitForSeconds(1f);

                ChangeState(GameState.Playing);

                break;
            case GameState.Playing:
                player.gameObject.SetActive(true);

                enemySpawner.enabled = true;
                propSpawner.enabled = true;

                break;
            case GameState.DeathScreen:
                deathScreen.Show();
                yield return new WaitForSeconds(5.5f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case GameState.WinScreen:
                winScreen.Show();
                yield return new WaitForSeconds(5.5f);
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
