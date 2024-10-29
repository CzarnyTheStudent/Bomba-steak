using System;
using Static;
using UnityEngine;

public class SetWorldBounds : MonoBehaviour
{
    private void Awake()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;
        BoundsGlobal.WorldBounds = bounds;
    }
}