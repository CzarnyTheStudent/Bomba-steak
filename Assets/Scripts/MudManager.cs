using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainParticles : MonoBehaviour
{
    public terrainEffectData tInter;
    private TerrainEffect _tEffect;

    // Słownik przechowujący cząsteczki dla różnych terenów
    private Dictionary<TerrainEffect.TerrainType, ParticleSystem> terrainParticlesDict;

    private void Start()
    {
        _tEffect = GetComponent<TerrainEffect>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.contacts[0]; 
            Vector2 collisionPosition = contact.point; 
                
            if (tInter.terrainParticle != null)
            {
                ParticleSystem particleGameobject = Instantiate(tInter.terrainParticle.gameObject, collisionPosition, Quaternion.identity).GetComponent<ParticleSystem>();
                particleGameobject.Play(); 
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeactivateParticles(); // Wyłączenie cząsteczek przy opuszczeniu terenu
        }
    }

    private void DeactivateParticles()
    {
        if (tInter.terrainParticle != null)
        {
            tInter.terrainParticle.Stop(); // Zatrzymanie aktywnych cząsteczek
        }
    }
}