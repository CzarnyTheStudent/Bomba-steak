using System;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private PlayerLineRenderer _playerLineRenderer;
        private PlayerStats _playerStats;
        private PlayerSound _playerSound;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _playerLineRenderer = GetComponent<PlayerLineRenderer>();
            _playerStats = GetComponent<PlayerStats>();
            _playerSound = GetComponent<PlayerSound>();
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
            _playerInput.enabled = false;
        }

        private void EnableControls()
        {
            _playerInput.enabled = true;
        }
    }
}