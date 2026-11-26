using System;
using System.IO;
using UnityEngine;
using Zenject;

public class JsonSaver : ISaver
{
    [Serializable]
    private struct ScoreData
    {
        public int score;
    }

    private readonly string _defaultPath;

    [Inject]
    public JsonSaver([Inject(Id = "SavePath", Optional = true)] string defaultPath = null)
    {
        _defaultPath = defaultPath;
    }

    public void SaveScore(int score, string path = null)
    {
        string targetPath = !string.IsNullOrWhiteSpace(path) ? path : _defaultPath;

        if (string.IsNullOrWhiteSpace(targetPath))
        {
            throw new ArgumentException("Path must be provided for JsonSaver.", nameof(path));
        }

        string directory = Path.GetDirectoryName(targetPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonUtility.ToJson(new ScoreData { score = score }, true);
        File.WriteAllText(targetPath, json);
        Debug.Log($"[JsonSaver] Saved score {score} to '{targetPath}'");
    }
}