using GameTools;
using UnityEngine;
using Player;

public class GetTerrainEffect : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    private PlayerMovement _playerMovement;
    private PlayerState playerState;
    private TerrainEffectData currentTerrain;
    private bool hasPlayedSound = false;
    private float defaultAngularDrag;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();
        defaultAngularDrag = _playerMovement.rb.angularDrag;
    }

    private void Update()
    {
        if (currentTerrain == null)
        {
            hasPlayedSound = false;
        }
    }

    private void ModifyAngularDrag(float newAngularDrag)
    {
        _playerMovement.rb.angularDrag = newAngularDrag;
    }

    public void ResetAngularDrag()
    {
        _playerMovement.rb.angularDrag = defaultAngularDrag;
        currentTerrain = null;
    }

    public void ApplyTerrainEffect(TerrainEffectData terrain)
    {
        if (currentTerrain != terrain)
        {
            ModifyAngularDrag(terrain.angularDrag);
            currentTerrain = terrain;

            if (!hasPlayedSound && playerState.IsPlayerMoving() && GameManager.Instance.GameReady)
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