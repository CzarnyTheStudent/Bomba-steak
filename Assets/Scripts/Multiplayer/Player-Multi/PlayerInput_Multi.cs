using Fusion;
using Player;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerInputMulti : NetworkBehaviour
    {
        private Vector3 _dragStartPos;
        private bool _isDragging;
        private PlayerMovementMulti _playerMovement;
        private PlayerLineRenderer _lineRenderer;
        private PlayerStats _playerStats;
        private PlayerAudio _audioObserver;
        private PlayerCooldownShoot _shootCooldown;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovementMulti>();
            _lineRenderer = GetComponent<PlayerLineRenderer>();
            _playerStats = GetComponent<PlayerStats>();
            _audioObserver = GetComponent<PlayerAudio>();
            _shootCooldown = GetComponent<PlayerCooldownShoot>();
        }

        private void Update()
{
    if (!PlayerMulti.isReady) return;
    if (!Object.HasInputAuthority || Input.touchCount <= 0) return;

    Touch touch = Input.GetTouch(0);
    Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));

    if (touch.phase == TouchPhase.Began)
    {
        DragStart(touchPos);
    }
    if (_isDragging && touch.phase == TouchPhase.Moved)
    {
        Dragging(touchPos);
    }
    if (_isDragging && touch.phase == TouchPhase.Ended)
    {
        DragRelease(touchPos);
    }
}

private void DragStart(Vector3 touchPos)
{
    if (!_shootCooldown.shootReady) return;
    _isDragging = true;
    _dragStartPos = transform.position; // Pozycja obiektu jako punkt startowy
    _lineRenderer.StartLine(_dragStartPos);
    _audioObserver.PlayDragStartSound();
}

private void Dragging(Vector3 touchPos)
{
    _lineRenderer.UpdateLine(touchPos);
    _audioObserver.PlayDraggingSound();

    Vector3 dragVector = touchPos - _dragStartPos; // Wektor przeciągnięcia
    float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, _playerMovement.maxDrag);
    Vector3 clampedDragVector = dragVector.normalized * dragDistance;

    Vector3 correctedEndPos = _dragStartPos + clampedDragVector;
    Debug.DrawRay(_dragStartPos, clampedDragVector, Color.yellow);
}

private void DragRelease(Vector3 touchPos)
{
    _isDragging = false;
    _lineRenderer.ClearLine();
    _audioObserver.PlayDragReleaseSound();
    _audioObserver.StopDraggingSound();

    Vector3 dragVector = touchPos - _dragStartPos; // Oblicz kierunek od startu do końca
    float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, _playerMovement.maxDrag);
    Vector3 clampedDragVector = dragVector.normalized * dragDistance;

    Vector3 correctedEndPos = _dragStartPos + clampedDragVector;
    Debug.DrawRay(_dragStartPos, clampedDragVector, Color.red);

    RpcApplyForce(_dragStartPos, correctedEndPos);
    StartCoroutine(_shootCooldown.WaitForShoot());
}

[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
private void RpcApplyForce(Vector3 startPos, Vector3 endPos)
{
    _playerMovement.ApplyForce(startPos, endPos);
}

    }
}
