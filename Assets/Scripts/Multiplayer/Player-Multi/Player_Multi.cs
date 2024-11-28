using Player;
using Static;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerMulti : MonoBehaviour
    {
        private PlayerInputMulti _playerInput;
        public static bool isReady;
        
        public void SetReady(bool set) => isReady = set;
        private void Start()
        {
            _playerInput = GetComponent<PlayerInputMulti>();
            DisableControls();
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
