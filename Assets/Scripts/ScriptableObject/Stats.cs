using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "NewStats", menuName = "Stats/PlayerStats")]
    public class Stats : UnityEngine.ScriptableObject
    {
        public string totalTime;
        public int dragCount = 0;
        public bool playerWon = false;

        // Ścieżka zapisu na podstawie platformy
        private string SaveFolder
        {
            get
            {
                #if UNITY_ANDROID
                return Path.Combine(Application.persistentDataPath, "Saves");  // Android
                #else
                return Path.Combine(Application.dataPath, "Saves");  // Windows/PC
                #endif
            }
        }

        private string SaveFileName => $"{SceneManager.GetActiveScene().name}.json";
        private string SaveFilePath => Path.Combine(SaveFolder, SaveFileName);

        public void UpdateTotalTime(string additionalTime)
        {
            if (string.IsNullOrEmpty(totalTime))
            {
                totalTime = additionalTime;
                Debug.Log($"Total time initialized to: {totalTime}");
                return;
            }

            if (!TimeSpan.TryParseExact(additionalTime, @"mm\:ss\:ff", null, out TimeSpan additionalTimeSpan) ||
                !TimeSpan.TryParseExact(totalTime, @"mm\:ss\:ff", null, out TimeSpan totalTimeSpan)) return;
            if (additionalTimeSpan > totalTimeSpan)
            {
                totalTime = additionalTime;
                Debug.Log($"Total time updated to: {totalTime}");
            }
        }
        
        public void UpdateDragCount(int newDragCount)
        {
            if (dragCount != 0) { if (newDragCount > dragCount) return; }
            dragCount = newDragCount;
        }
        
        public void UpdateLevelComplete(bool won)
        {
            if (!won) return;
            playerWon = won;
            Debug.Log($"Player won status updated to: {playerWon}");
        }
        
        public void Save()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }

            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(SaveFilePath, json);
            Debug.Log($"Data saved to {SaveFilePath}");
        }

        public void Load()
        {
            if (File.Exists(SaveFilePath))
            {
                string json = File.ReadAllText(SaveFilePath);
                JsonUtility.FromJsonOverwrite(json, this);
                Debug.Log($"Data loaded from {SaveFilePath}");
            }
            else
            {
                Debug.LogWarning($"Save file not found: {SaveFilePath}");
            }
        }
    }
}
