using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveFileName = "player_progress.json";

    private static string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    public static void Save(GameData data)
    {
        if (data == null)
        {
            data = new GameData();
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Saved game data to: " + SavePath);
    }

    public static GameData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("Save file not found. Creating new game data.");
            return new GameData();
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            if (data == null)
            {
                Debug.LogWarning("Save file was invalid. Using default game data.");
                return new GameData();
            }

            if (data.unlockedLevel < 1)
            {
                data.unlockedLevel = 1;
            }

            Debug.Log("Loaded game data from: " + SavePath);
            return data;
        }
        catch (Exception exception)
        {
            Debug.LogWarning("Failed to load save file: " + exception.Message);
            return new GameData();
        }
    }
}