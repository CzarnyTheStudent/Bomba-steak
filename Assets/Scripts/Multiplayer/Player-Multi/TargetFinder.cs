using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private CameraMovement cam;
    private static TargetFinder _singleton;

    public static TargetFinder Singleton
    {
        get => _singleton;
        private set
        {
            if (value == null)
            {
                _singleton = null;
            }
            else if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Singleton = this;
    }

    private void OnDestroy()
    {
        if (Singleton == this)
        {
            Singleton = null;
        }
    }


    public void SetTarget(Transform newTarget)
    {
        cam.target = newTarget;
    }
}
