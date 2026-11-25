using System;
using System.IO;
using UnityEngine;

public class JsonSaver : ISaver
{
    [Serializable]
    private struct ScoreData
    {
        public int score;
    }

    public void SaveScore(int score, string path = null)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path must be provided for JsonSaver.", nameof(path));
        }

        string directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonUtility.ToJson(new ScoreData { score = score }, true);
        File.WriteAllText(path, json);
        Debug.Log($"[JsonSaver] Saved score {score} to '{path}'");
    }
}