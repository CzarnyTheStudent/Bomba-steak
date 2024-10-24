using Static;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTools
{
    public class LevelMusicController : MonoBehaviour
    {
        public static LevelMusicController Instance { get; private set; }

        [SerializeField] private GameMusicSettings gameMusicSettings;
        private string currentLevelName;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            EventManager.OnLevelStart += PlayCalmMusic;
            EventManager.OnPinPulled += PlayActionMusic;
            EventManager.OnLevelEnd += PlayTransitionMusic;
        }

        private void OnDestroy()
        {
            EventManager.OnLevelStart -= PlayCalmMusic;
            EventManager.OnPinPulled -= PlayActionMusic;
            EventManager.OnLevelEnd -= PlayTransitionMusic;
        }

        private void PlayCalmMusic()
        {
            currentLevelName = SceneManager.GetActiveScene().name;
            foreach (var level in gameMusicSettings.levelsMusic)
            {
                if (level.levelName == currentLevelName)
                {
                    MusicManager.Instance.PlayMusic(level.startLevelMusic, 1.0f);
                    return;
                }
            }
        }

        private void PlayActionMusic()
        {
            foreach (var level in gameMusicSettings.levelsMusic)
            {
                if (level.levelName == currentLevelName)
                {
                    MusicManager.Instance.PlayMusic(level.gameMusic, 1.0f, true); // Szybka zmiana muzyki
                    return;
                }
            }
        }

        private void PlayTransitionMusic()
        {
            foreach (var level in gameMusicSettings.levelsMusic)
            {
                if (level.levelName == currentLevelName)
                {
                    MusicManager.Instance.PlayMusic(level.transitionMusic, 0.5f); // Muzyka miêdzy poziomami
                    return;
                }
            }
        }
    }
}
