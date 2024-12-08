using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private CameraMovement cam;
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
        cam = GetComponent<CameraMovement>();
        Singleton = this;
    }

    private void OnDestroy()
    {
        if (Singleton == this)
        {
            Singleton = null;
        }
    }
    
    public void SetTarget(Transform newTarget) => cam.target = newTarget;
}
