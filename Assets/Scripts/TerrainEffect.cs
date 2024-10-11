using UnityEngine;

public class TerrainEffect : MonoBehaviour
{
    public enum TerrainType { Puddle, Mud, Wood }
    public TerrainType terrainType;

    public float puddleAngularDrag ; 
    public float mudAngularDrag; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetTerrainEffect getEffect = collision.gameObject.GetComponent<GetTerrainEffect>();
            if (getEffect != null)
            {
                ApplyEffect(getEffect);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetTerrainEffect getTerrain = collision.gameObject.GetComponent<GetTerrainEffect>();
            if (getTerrain != null)
            {
                getTerrain.ResetAngularDrag(); 
            }
        }
    }

    private void ApplyEffect(GetTerrainEffect getEffect)
    {
        switch (terrainType)
        {
            case TerrainType.Puddle:
                getEffect.ModifyAngularDrag(puddleAngularDrag);
                break;
            case TerrainType.Mud:
                getEffect.ModifyAngularDrag(mudAngularDrag);
                break;
            case TerrainType.Wood:
                break;
        }
    }
}