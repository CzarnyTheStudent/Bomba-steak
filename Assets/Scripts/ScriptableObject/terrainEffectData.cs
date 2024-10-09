using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "New Interaction", menuName = "TerrainInteraction/NewInteraction")]
    public abstract class TerrainEffectData : UnityEngine.ScriptableObject
    {
        public ParticleSystem terrainParticle;
        public AudioClip sound;
        
    }
}
