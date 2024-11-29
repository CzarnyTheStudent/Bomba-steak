using Player;
using Static;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerMulti : MonoBehaviour
    {
        private int _playerId;
        private PlayerInputMulti _playerInput;
        public static bool isReady;
        
        public void SetPlayerId(int playerId)
        {
            _playerId = playerId;
            Debug.Log($"Player ID set to: {_playerId}");
        }

        public int GetPlayerId()
        {
            return _playerId;
        }
        public void SetReady(bool set) => isReady = set;
        private void Start()
        {
            _playerInput = GetComponent<PlayerInputMulti>();
            DisableControls();
        }
        
        public void UpdateStats(bool won)
        {
            PlayerStatsCollectorMulti.instance.IncrementDragEndCount(_playerId);
            PlayerStatsCollectorMulti.instance.UpdateCurrentTime(_playerId);
            PlayerStatsCollectorMulti.instance.SetPlayerWon(_playerId, won); 
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
            if (!isReady) return;
            _playerInput.enabled = false;
        }

        private void EnableControls()
        {
            if (!isReady) return;
            _playerInput.enabled = true;
        }
    }
}
