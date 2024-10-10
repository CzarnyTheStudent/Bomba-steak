using Static;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Variables

    public Transform target;  // Obiekt, który kamera ma śledzić (np. gracz)
    public float smoothSpeed = 0.125f;  // Prędkość płynnego podążania
    public Vector3 offset;  // Przesunięcie kamery względem obiektu

    private Camera _mainCamera;
    private Bounds _cameraBounds;

    #endregion

    private void Awake() => _mainCamera = Camera.main;

    private void Start()
    {
        // Obliczamy granice kamery w zależności od rozmiaru okna gry
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        // Ustawiamy granice świata
        var minX = BoundsGlobal.WorldBounds.min.x + width;
        var maxX = BoundsGlobal.WorldBounds.extents.x - width;

        var minY = BoundsGlobal.WorldBounds.min.y + height;
        var maxY = BoundsGlobal.WorldBounds.extents.y - height;

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
            FollowTarget();  // Śledzenie obiektu (gracza)
        }
    }

    private void FollowTarget()
    {
        // Docelowa pozycja kamery
        Vector3 desiredPosition = target.position + offset;
        // Płynne przejście do nowej pozycji
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Upewniamy się, że pozycja kamery mieści się w granicach
        smoothedPosition = GetCameraBounds(smoothedPosition);
        // Ustawienie pozycji kamery
        transform.position = smoothedPosition;
    }

    // Metoda sprawdzająca granice kamery
    private Vector3 GetCameraBounds(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(position.y, _cameraBounds.min.y, _cameraBounds.max.y),
            position.z
        );
    }
}
