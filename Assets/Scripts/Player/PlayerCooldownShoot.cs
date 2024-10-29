using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownShoot : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 4;
    public bool shootReady = true;

    public IEnumerator WaitForShoot()
    {
        shootReady = false;
        yield return new WaitForSeconds(cooldownTime);
        shootReady = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Terrain") )
        {
            shootReady = true;
        }
    }
}
