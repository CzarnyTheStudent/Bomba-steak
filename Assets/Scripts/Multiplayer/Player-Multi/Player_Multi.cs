using Fusion;
using Static;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerMulti : NetworkBehaviour
    {
        private int _playerId;
        private PlayerInputMulti _playerInput;
        
        public void SetPlayerId(int playerId)
        {
            _playerId = playerId;
            Debug.Log($"Player ID set to: {_playerId}");
        }

        public int GetPlayerId() => _playerId;

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                TargetFinder.Singleton.SetTarget(transform);
            }
        }
        
        private void Start()
        {
            _playerInput = GetComponent<PlayerInputMulti>();
        }

        private void OnEnable()
        {
            EventManager.GameStart += EnableControls;
        }

        private void OnDisable()
        {
            EventManager.GameStart -= EnableControls;
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
