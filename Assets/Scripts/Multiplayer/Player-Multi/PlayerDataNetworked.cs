using Fusion;
using UI;
using UnityEngine;

public class PlayerDataNetworked : NetworkBehaviour
    {
        // Local Runtime references
        private GameOverUIManagerMulti _overviewPanel;
        private ChangeDetector _changeDetector;
        public static PlayerDataNetworked instance;

        private void Awake() => instance = this;
        

        // Game Session SPECIFIC Settings are used in the UI.
        // The method passed to the OnChanged attribute is called everytime the [Networked] parameter is changed.
        [Networked]
        public NetworkString<_16> NickName { get; private set; }
        
        [Networked]
        public string Time { get; private set; }
        
        [Networked]
        public int DragCount { get; private set; }
        
        [Networked]
        public bool PlayerWon { get; private set; }

        public override void Spawned()
        {
            // --- Client
            // Find the local non-networked PlayerData to read the data and communicate it to the Host via a single RPC 
            if (Object.HasInputAuthority)
            {
                var nickName = FindObjectOfType<PlayerData>().GetNickName();
                RpcSetNickName(nickName);
            }

            // --- Host
            // Initialized game specific settings
            if (Object.HasStateAuthority)
            {
                Time = "00:00:00";
                DragCount = 0;
                PlayerWon = false;
            }

            // --- Host & Client
            // Set the local runtime references.
            _overviewPanel = FindObjectOfType<GameOverManager>().gameOverUiMulti;
            // Add an entry to the local Overview panel with the information of this spaceship
            if (!_overviewPanel) return;
            _overviewPanel.AddEntry(Object.InputAuthority, this);
            
            // Refresh panel visuals in Spawned to set to initial values.
            _overviewPanel.UpdateNickName(Object.InputAuthority, NickName.ToString());
            _overviewPanel.UpdateTime(Object.InputAuthority, Time);
            _overviewPanel.UpdateDragCount(Object.InputAuthority, DragCount);
            _overviewPanel.UpdateWon(Object.InputAuthority, PlayerWon);
            
            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        }
        
        public override void Render()
        {
            if (_changeDetector == null) return;
            foreach (var change in _changeDetector.DetectChanges(this, out var previousBuffer, out var currentBuffer))
            {
                switch (change)
                {
                    case nameof(NickName):
                        _overviewPanel.UpdateNickName(Object.InputAuthority, NickName.ToString());
                        break;
                    case nameof(DragCount):
                        _overviewPanel.UpdateDragCount(Object.InputAuthority, DragCount);
                        break;
                    case nameof(Time):
                        _overviewPanel.UpdateTime(Object.InputAuthority, Time);
                        break;
                    case nameof(PlayerWon):
                        _overviewPanel.UpdateWon(Object.InputAuthority, PlayerWon);
                        break;
                }
            }
        }

        // Remove the entry in the local Overview panel for this spaceship
        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _overviewPanel.RemoveEntry(Object.InputAuthority);
        }

        public void SetPlayerWon()
        {
            PlayerWon = true;
        }
        
        // Increase the score by X amount of points
        public void AddDragToCount()
        {
            DragCount++;
        }
        
        public void SetTime()
        {
            Time = Timer.instance.GetCurrentTime();
        }

        // RPC used to send player information to the Host
        [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
        private void RpcSetNickName(string nickName)
        {
            if (string.IsNullOrEmpty(nickName)) return;
            NickName = nickName;
        }
    }