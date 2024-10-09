using System.Collections.Generic;
using ScriptableObject;
using UnityEngine;

public class TerrainParticles : MonoBehaviour
{
    public TerrainEffectData tInter;
    private ParticleSystem _particleInst;

    // Słownik przechowujący cząsteczki dla różnych terenów
    private Dictionary<TerrainEffect.TerrainType, ParticleSystem> terrainParticlesDict;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 collisionPosition = contact.point;

            if (tInter.terrainParticle != null)
            {
                if (!_particleInst)
                {
                    Destroy(_particleInst);
                }
                ParticleSystem particleGameobj = Instantiate(tInter.terrainParticle.gameObject, collisionPosition, Quaternion.identity).GetComponent<ParticleSystem>();
                _particleInst = particleGameobj;
                particleGameobj.Play();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collisionInfo)
    {
            ContactPoint2D contact = collisionInfo.contacts[0];
            Vector2 collisionPosition = contact.point;
            _particleInst.transform.position = collisionPosition;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (tInter.terrainParticle != null)
            {
                tInter.terrainParticle.Stop(); 
                Destroy(_particleInst);
            }
        }
    }
}