using UnityEngine;

[CreateAssetMenu(fileName = "TerrainEffectData", menuName = "TerrainEffects/NewTerrainEffect")]
public class TerrainEffectData : ScriptableObject
{
    public float angularDrag;
    public AudioClip terrainSound;
}