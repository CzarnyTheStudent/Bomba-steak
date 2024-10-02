using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "TerrainInteraction/NewInteraction")]
public class terrainEffectData : ScriptableObject
{
    public ParticleSystem terrainParticle;
    public AudioClip sound;
}
