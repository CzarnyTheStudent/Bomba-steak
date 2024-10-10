using System.IO;
using UnityEngine;

public class SaveManager
{
    private static string SaveFolder
    {
        get
        {
            #if UNITY_ANDROID
            return Path.Combine(Application.persistentDataPath, "Saves");
            #else
            return Path.Combine(Application.dataPath, "Saves");
            #endif
        }
    }

    private static string GetSaveFilePath(string fileName)
    {
        return Path.Combine(SaveFolder, $"{fileName}.json");
    }

    public static void Save<T>(T data, string fileName)
    {
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }

        string json = JsonUtility.ToJson(data, true);
        string filePath = GetSaveFilePath(fileName);
        File.WriteAllText(filePath, json);
        Debug.Log($"Data saved to {filePath}");
    }

    public static T Load<T>(string fileName) where T : new()
    {
        string filePath = GetSaveFilePath(fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"Data loaded from {filePath}");
            return data;
        }
        else
        {
            Debug.LogWarning($"Save file not found: {filePath}, creating new data.");
            return new T(); 
        }
    }
}