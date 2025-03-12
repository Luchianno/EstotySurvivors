using UnityEngine;

// static menu box buttons for editor
public static class EditorMenu
{
    // reset hight score
    [MenuItem("Survivors/Reset High Score")]
    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt(PlayerStatsManager.PrefsPath, 0);
        PlayerPrefs.Save();
    }

    // reset player prefs
    [MenuItem("💀 Survivors/Reset Player Prefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // set high score to 10 
    [MenuItem("💀 Survivors/Set High Score to 10")]
    public static void SetHighScoreTo10()
    {
        PlayerPrefs.SetInt(PlayerStatsManager.PrefsPath, 10);
        PlayerPrefs.Save();
    }
}
