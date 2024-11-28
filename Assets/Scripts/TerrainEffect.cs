using UnityEngine;


public interface ITerrainEffectHandler
{
    void ApplyTerrainEffect(TerrainEffectData terrain);
    void ResetAngularDrag();
}



public class TerrainEffect : MonoBehaviour
{
    public TerrainEffectData terrainData;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ITerrainEffectHandler effectHandler = collision.gameObject.GetComponent<ITerrainEffectHandler>();
            if (effectHandler != null && terrainData != null)
            {
                effectHandler.ApplyTerrainEffect(terrainData);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ITerrainEffectHandler effectHandler = collision.gameObject.GetComponent<ITerrainEffectHandler>();
            if (effectHandler != null)
            {
                effectHandler.ResetAngularDrag();
            }
        }
    }
}