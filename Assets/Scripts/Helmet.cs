using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Helmet : MonoBehaviour
{
    public List<Door> doors; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var door in doors)
            {
                door.StartMovement();
            }
        }
    }
}