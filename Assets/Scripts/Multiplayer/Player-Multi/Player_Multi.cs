using Fusion;
using Player;
using Static;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerMulti : NetworkBehaviour
    {
        private int _playerId;
        private PlayerInputMulti _playerInput;
        
        // Game Session SPECIFIC Settings
        public bool AcceptInput => _isReady && Object.IsValid;
        
        [Networked] private NetworkBool _isReady { get; set; }
        
        public void SetPlayerId(int playerId)
        {
            _playerId = playerId;
            Debug.Log($"Player ID set to: {_playerId}");
        }

        public int GetPlayerId() => _playerId;
        
        private void Start()
        {
            // --- Host & Client
            // Set the local runtime references.
            _playerInput = GetComponent<PlayerInputMulti>();
            NetworkObject playergigachad = Runner.GetPlayerObject(Runner.LocalPlayer);
            TargetFinder.Singleton.SetTarget(playergigachad.transform);

            // --- Host
            // The Game Session SPECIFIC settings are initialized
            //if (Object.HasStateAuthority) return;
            _isReady = true;
        }

     

        private void DisableControls()
        {
            _playerInput.enabled = false;
        }

        private void EnableControls()
        {
            _playerInput.enabled = true;
        }
    }
}
