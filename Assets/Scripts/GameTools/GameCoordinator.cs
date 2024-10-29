using UnityEngine;

namespace GameTools
{
    public class Coordinator : MonoBehaviour
    {
        public static Coordinator Instance { get; private set; }
        private GameSetup _gameSetup;
        private bool _isGameSetupReady = false;

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

        public void RegisterGameSetup(GameSetup gameSetup)
        {
            _gameSetup = gameSetup;
            _isGameSetupReady = true;
        }
        
        public GameSetup GetGameSetup()
        {
            return _gameSetup;
        }

        public bool IsGameSetupReady() => _isGameSetupReady;
    }
}