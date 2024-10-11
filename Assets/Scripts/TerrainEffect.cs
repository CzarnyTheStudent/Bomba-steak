using UnityEngine;

public class TerrainEffect : MonoBehaviour
{
    public TerrainEffectData terrainData;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetTerrainEffect getEffect = collision.gameObject.GetComponent<GetTerrainEffect>();
            if (getEffect != null && terrainData != null)
            {
                getEffect.ApplyTerrainEffect(terrainData);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetTerrainEffect getEffect = collision.gameObject.GetComponent<GetTerrainEffect>();
            if (getEffect != null)
            {
                getEffect.ResetAngularDrag();
            }
        }
    }
}