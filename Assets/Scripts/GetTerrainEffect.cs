using UnityEngine;
using Player;

public class GetTerrainEffect : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private AudioSource audioSource;
    private PlayerState playerState;
    private TerrainEffectData currentTerrain;
    private bool hasPlayedSound = false;
    private float defaultAngularDrag;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();
        audioSource = GetComponent<AudioSource>();
        defaultAngularDrag = _playerMovement.rb.angularDrag;
    }

    private void Update()
    {
        if (currentTerrain == null)
        {
            hasPlayedSound = false;
        }
    }

    public void ModifyAngularDrag(float newAngularDrag)
    {
        _playerMovement.rb.angularDrag = newAngularDrag;
    }

    public void ResetAngularDrag()
    {
        _playerMovement.rb.angularDrag = defaultAngularDrag;
        currentTerrain = null; // Reset the current terrain when leaving it
    }

    public void ApplyTerrainEffect(TerrainEffectData terrain)
    {
        if (currentTerrain != terrain)
        {
            ModifyAngularDrag(terrain.angularDrag);
            currentTerrain = terrain;

            if (!hasPlayedSound && playerState.IsPlayerMoving())
            {
                PlaySound(terrain.terrainSound);
                hasPlayedSound = true;
            }
        }
    }

    private void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}