using UnityEngine;

[CreateAssetMenu(fileName = "AppSettings", menuName = "💀 Survivors/AppSettings")]
public class AppSettings : ScriptableObject
{
    const string MusicVolumePreference = "MusicVolume";
    const string SFXVolumePreference = "SFXVolume";
    const string HighScorePreference = "HighScore";
    const string PreferredFrameRatePreference = "PreferredFrameRate";

    [Range(0f, 1f)]
    public float MusicVolume = 0.6f;
    [Range(0f, 1f)]
    public float SFXVolume = 0.6f;
    [Min(0)]
    public int HighScore = 0;

    [Min(1)]
    public int PreferredFrameRate = 60;

    public void LoadFromPreferences()
    {
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumePreference, MusicVolume);
        SFXVolume = PlayerPrefs.GetFloat(SFXVolumePreference, SFXVolume);
        HighScore = PlayerPrefs.GetInt(HighScorePreference, HighScore);
        PreferredFrameRate = PlayerPrefs.GetInt(PreferredFrameRatePreference, PreferredFrameRate);
    }

    public void SaveToPreferences()
    {
        PlayerPrefs.SetFloat(MusicVolumePreference, MusicVolume);
        PlayerPrefs.SetFloat(SFXVolumePreference, SFXVolume);
        PlayerPrefs.SetInt(HighScorePreference, HighScore);
        PlayerPrefs.SetInt(PreferredFrameRatePreference, PreferredFrameRate);
        PlayerPrefs.Save();
    }
}
