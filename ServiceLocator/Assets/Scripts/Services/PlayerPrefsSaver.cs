using UnityEngine;

public class PlayerPrefsSaver : ISaver
{
    private const string DefaultKey = "SavedScore";

    public void SaveScore(int score, string path = null)
    {
        string key = string.IsNullOrWhiteSpace(path) ? DefaultKey : path;
        PlayerPrefs.SetInt(key, score);
        PlayerPrefs.Save();
        Debug.Log($"[PlayerPrefsSaver] Saved score {score} under key '{key}'");
    }
}