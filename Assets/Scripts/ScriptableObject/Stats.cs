using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "NewStats", menuName = "Stats/PlayerStats")]
    public class Stats : UnityEngine.ScriptableObject
    {
        public int timePlayed;
        public string totalTime;
        public int dragCount = 0;
        public bool playerWon = false;

        private string SaveFolder => Path.Combine(Application.dataPath, "Saves");
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

            // Parsujemy obecny i nowy czas
            if (TimeSpan.TryParseExact(additionalTime, @"mm\:ss\:ff", null, out TimeSpan additionalTimeSpan) &&
                TimeSpan.TryParseExact(totalTime, @"mm\:ss\:ff", null, out TimeSpan totalTimeSpan))
            {
                // Nadpisujemy czas, jeśli nowy czas jest większy
                if (additionalTimeSpan > totalTimeSpan)
                {
                    totalTime = additionalTime;
                    Debug.Log($"Total time updated to: {totalTime}");
                }
                else
                {
                    Debug.Log("New time is not greater than the current total time.");
                }
            }
            else
            {
                Debug.LogError("Failed to parse one or both of the times.");
            }
        }
        
        public void UpdateDragCount(int newDragCount)
        {
            if (newDragCount > dragCount) return; 
            dragCount = newDragCount; ;
        }

        // Poprawiona metoda UpdateLevelComplete
        public void UpdateLevelComplete(bool won)
        {
            if (!won) return;
            playerWon = won;
            Debug.Log($"Player won status updated to: {playerWon}");
            
        }

        // Zapis danych do pliku
        public void Save()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }

            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(SaveFilePath, json);
        }

        // Odczyt danych z pliku
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
