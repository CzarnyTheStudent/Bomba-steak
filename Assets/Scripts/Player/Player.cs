using System;
using Static;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private PlayerLineRenderer _playerLineRenderer;
        private PlayerStats _playerStats;
        private PlayerAudio playerAudio;
        private GetTerrainEffect _terrainGet;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _playerLineRenderer = GetComponent<PlayerLineRenderer>();
            _playerStats = GetComponent<PlayerStats>();
            playerAudio = GetComponent<PlayerAudio>();
            _terrainGet = GetComponent<GetTerrainEffect>();
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

        private void DisableControls() => _playerInput.enabled = false;

        private void EnableControls() => _playerInput.enabled = true;
        
    }
}