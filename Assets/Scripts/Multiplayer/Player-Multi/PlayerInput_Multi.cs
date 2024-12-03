using Fusion;
using Player;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerInputMulti : NetworkBehaviour
    {
        private Vector3 _dragStartPos;
        private bool _isDragging;
        private PlayerMulti _playerMulti;
        private PlayerMovementMulti _playerMovement;
        private PlayerLineRenderer _lineRenderer;
        private PlayerStats _playerStats;
        private PlayerAudio _audioObserver;
        private PlayerCooldownShoot _shootCooldown;

        private void Start()
        {
            _playerMulti = GetComponent<PlayerMulti>();
            _playerMovement = GetComponent<PlayerMovementMulti>();
            _lineRenderer = GetComponent<PlayerLineRenderer>();
            _playerStats = GetComponent<PlayerStats>();
            _audioObserver = GetComponent<PlayerAudio>();
            _shootCooldown = GetComponent<PlayerCooldownShoot>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out NetworkInputData inputData)) return;

            if (inputData.IsDragging)
            {
                Dragging(inputData.DragEnd);
            }
            else if (!_isDragging && inputData.DragStart != Vector3.zero)
            {
                DragStart(inputData.DragStart);
            }
            else if (_isDragging && !inputData.IsDragging)
            {
                DragRelease(inputData.DragEnd);
            }
        }

        private void DragStart(Vector3 touchPos)
        {
            if (!_shootCooldown.shootReady) return;
            _isDragging = true;
            _dragStartPos = transform.position;
            _lineRenderer.StartLine(_dragStartPos);
            _audioObserver.PlayDragStartSound();
        }

        private void Dragging(Vector3 touchPos)
        {
            _lineRenderer.UpdateLine(touchPos);
            _audioObserver.PlayDraggingSound();

            Vector3 dragVector = touchPos - _dragStartPos;
            float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, _playerMovement.maxDrag);
            Vector3 clampedDragVector = dragVector.normalized * dragDistance;

            Vector3 correctedEndPos = _dragStartPos + clampedDragVector;
            Debug.DrawRay(_dragStartPos, clampedDragVector, Color.yellow);
        }

        private void DragRelease(Vector3 touchPos)
        {
            _isDragging = false;
            PlayerStatsCollectorMulti.instance.IncrementDragEndCount(_playerMulti.GetPlayerId());
            _lineRenderer.ClearLine();
            _audioObserver.PlayDragReleaseSound();
            _audioObserver.StopDraggingSound();

            Vector3 dragVector = touchPos - _dragStartPos;
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
