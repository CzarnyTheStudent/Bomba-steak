using Static;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Variables

    public Transform target;  
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    private Camera _mainCamera;
    private Bounds _cameraBounds;

    #endregion

    private void Awake() => _mainCamera = Camera.main;

    private void Start()
    {
        
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

       
        var minX = BoundsGlobal.WorldBounds.min.x + width;
        var maxX = BoundsGlobal.WorldBounds.max.x - width;

        var minY = BoundsGlobal.WorldBounds.min.y + height;
        var maxY = BoundsGlobal.WorldBounds.max.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();  
        }
    }

    private void FollowTarget()
    {
       
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
      
        smoothedPosition = GetCameraBounds(smoothedPosition);
        
        transform.position = smoothedPosition;
    }

   
    private Vector3 GetCameraBounds(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(position.y, _cameraBounds.min.y, _cameraBounds.max.y),
            position.z
        );
    }
}
