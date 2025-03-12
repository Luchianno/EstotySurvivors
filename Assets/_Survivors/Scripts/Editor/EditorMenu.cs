using UnityEditor;
using UnityEngine;

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

        var health = player.GetComponent<PlayerHealth>();

        health.ChangeBy(new HealthChange(-health.Current));
    }
}
