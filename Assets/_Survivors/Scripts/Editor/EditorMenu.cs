using UnityEditor;
using UnityEngine;
using Zenject;

// static menu box buttons for editor
public class EditorMenu
{
    // reset player prefs
    [MenuItem("💀 Survivors/Reset Player Prefs", priority = 10)]
    static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // reset hight score
    [MenuItem("💀 Survivors/Reset High Score", priority = 20)]
    static void ResetHighScore()
    {
        PlayerPrefs.SetInt(PlayerStatsManager.PrefsPath, 0);
        PlayerPrefs.Save();
    }


    // set high score to 10 
    [MenuItem("💀 Survivors/Set High Score to 10", priority = 30)]
    static void SetHighScoreTo10()
    {
        PlayerPrefs.SetInt(PlayerStatsManager.PrefsPath, 10);
        PlayerPrefs.Save();
    }

    [MenuItem("💀 Survivors/Kill Player", priority = 110)]
    static void KillPlayer()
    {
        GameObject player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        var health = player.GetComponent<PlayerHealth>();

        if (health == null)
        {
            Debug.LogError("PlayerHealth component not found on player");
            return;
        }

        if (!health.IsAlive)
        {
            Debug.Log("Player is already dead");
            return;
        }

        health.ChangeBy(new HealthChange(-health.Current));
    }

    [MenuItem("💀 Survivors/Level Up", priority = 120)]
    static void LevelUp()
    {
        GameObject player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        // get scene context
        var sceneContext = GameObject.FindFirstObjectByType<SceneContext>(FindObjectsInactive.Include);

        // get signal bus from zenject
        var signalBus = sceneContext.Container.Resolve<SignalBus>();
        var levelProgression = sceneContext.Container.Resolve<LevelProgression>();

        if (levelProgression.IsMaxLevel)
        {
            Debug.Log("Player is already max level");
            return;
        }

        // get enogh experience to level up
        var experience = levelProgression.NextLevel.ExperienceRequired - levelProgression.CurrentLevel.ExperienceRequired;

        signalBus.Fire(new AddExperienceSignal(experience));

    }
}
