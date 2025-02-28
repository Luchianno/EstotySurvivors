using UnityEngine;

public class AppSettings : ScriptableObject
{
    const string MusicVolumePreference = "MusicVolume";
    const string SFXVolumePreference = "SFXVolume";
    const string HighScorePreference = "HighScore";

    public float MusicVolume = 0.6f;
    public float SFXVolume = 0.6f;
    public int HighScore = 0;

    public void LoadFromPreferences()
    {
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumePreference, MusicVolume);
        SFXVolume = PlayerPrefs.GetFloat(SFXVolumePreference, SFXVolume);
        HighScore = PlayerPrefs.GetInt(HighScorePreference, HighScore);
    }

    public void SaveToPreferences()
    {
        PlayerPrefs.SetFloat(MusicVolumePreference, MusicVolume);
        PlayerPrefs.SetFloat(SFXVolumePreference, SFXVolume);
        PlayerPrefs.SetInt(HighScorePreference, HighScore);
        PlayerPrefs.Save();
    }
}
