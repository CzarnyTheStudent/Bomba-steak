using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownShoot : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 4;
    [HideInInspector] public bool shootReady = true;

    public IEnumerator WaitForShoot()
    {
        shootReady = false;
        yield return new WaitForSeconds(cooldownTime);
        shootReady = true;
    }
}
