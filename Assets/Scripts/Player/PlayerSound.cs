using UnityEngine;
using System.Collections.Generic;

namespace Player
{
    [System.Serializable]
    public class SoundEntry
    {
        public string soundType; 
        public AudioClip audioClip;
    }

    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<SoundEntry> soundEntries = new List<SoundEntry>();

        private Dictionary<string, AudioClip> soundDictionary;

        private void Start()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            soundDictionary = new Dictionary<string, AudioClip>();
            foreach (var entry in soundEntries)
            {
                if (!soundDictionary.ContainsKey(entry.soundType))
                {
                    soundDictionary.Add(entry.soundType, entry.audioClip);
                }
            }
        }

        public void PlaySound(string soundType)
        {
            if (soundDictionary.TryGetValue(soundType, out AudioClip clip))
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"Sound type {soundType} not found in the dictionary.");
            }
        }

        public void StopAllSounds()
        {
            audioSource.Stop();
        }
    }
}