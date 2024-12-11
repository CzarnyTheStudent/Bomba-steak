using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using GameTools;
using Multiplayer.Player_Multi;
using Player;
using Static;
using TMPro;
using UnityEngine;

    public class GameStateController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
    {
        enum GameState
        {
            Starting,
            Running,
            Ending
        }

        [SerializeField] private float _startDelay = 4.0f;
        [SerializeField] private TextMeshProUGUI _startEndDisplay = null;

        [Networked] private TickTimer _timer { get; set; }
        [Networked] private GameState _gameState { get; set; }

        [Networked] private NetworkBehaviourId _winner { get; set; }

        private List<NetworkBehaviourId> _playerDataNetworkedIds = new List<NetworkBehaviourId>();

        private int playerCount;

        public override void Spawned()
        {
            // --- This section is for all information which has to be locally initialized based on the networked game state
            // --- when a CLIENT joins a game

            _startEndDisplay.gameObject.SetActive(true);

            // If the game has already started, find all currently active players' PlayerDataNetworked component Ids
            if (_gameState != GameState.Starting)
            {
                foreach (var player in Runner.ActivePlayers)
                {
                    if (Runner.TryGetPlayerObject(player, out var playerObject) == false) continue;
                    TrackNewPlayer(playerObject.GetComponent<PlayerMulti>().Id);
                }
            }

            // Set is Simulated so that FixedUpdateNetwork runs on every client instead of just the Host
            Runner.SetIsSimulated(Object, true);
            GameModeManager.CurrentGameMode = GameModeManager.GameMode.Multiplayer;

            // --- This section is for all networked information that has to be initialized by the HOST
            if (!Object.HasStateAuthority) return;

            // Initialize the game state on the host
            _gameState = GameState.Starting;
            _timer = TickTimer.CreateFromSeconds(Runner, _startDelay);
    }

        public override void FixedUpdateNetwork()
        {
            switch (_gameState)
            {
                case GameState.Starting:
                    UpdateStartingDisplay();
                    break;
                case GameState.Running:
                    UpdateRunningDisplay();
                    break;
                case GameState.Ending:
                    UpdateEndingDisplay();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateStartingDisplay()
        {
            // --- Host & Client
            // Display the remaining time until the game starts in seconds (rounded down to the closest full second)
            _startEndDisplay.text = $"Game Starts In {Mathf.RoundToInt(_timer.RemainingTime(Runner) ?? 0)}";

            // --- Host
            if (Object.HasStateAuthority == false) return;
            if (_timer.ExpiredOrNotRunning(Runner) == false) return;
            while (playerCount != 2) return;
            
            FindObjectOfType<PlayerSpawner>().StartPlayerSpawner(this);
            InitializeGame();

            // Switches to the Running GameState and sets the time to the length of a game session
            _gameState = GameState.Running;
        }

        private void UpdateRunningDisplay()
        {
            // --- Host & Client
            // Display the remaining time until the game ends in seconds (rounded down to the closest full second)
            _startEndDisplay.gameObject.SetActive(false);
    }

        private void UpdateEndingDisplay()
        {
            // --- Host & Client
            // Display the results and
            // the remaining time until the current game session is shutdown
            if (Runner.TryFindBehaviour(_winner, out PlayerMulti playerData) == false) return;
          

            // --- Host
            // Shutdowns the current game session.
            // The disconnection behaviour is found in the OnServerDisconnect.cs script
            Runner.Shutdown();
        }
        
        public void CheckIfGameHasEnded()
        {
            GameHasEnded();
        }

    private void InitializeGame()
    {
        Timer.instance.SetToStopwatch();
        EventManager.OnTimerStart();
        
        EventManager.OnGameStart();
    }

    private void GameHasEnded()
        {
            _gameState = GameState.Ending;
        }

        public void TrackNewPlayer(NetworkBehaviourId playerDataNetworkedId)
        {
            _playerDataNetworkedIds.Add(playerDataNetworkedId);
        }

    public void PlayerJoined(PlayerRef player)
    {
        playerCount++;
    }

    public void PlayerLeft(PlayerRef player)
    {
        playerCount--;
    }
}
