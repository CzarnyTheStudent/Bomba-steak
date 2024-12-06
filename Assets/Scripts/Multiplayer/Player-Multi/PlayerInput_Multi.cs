using Fusion;
using Player;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerInputMulti : NetworkBehaviour
    {
        private Vector3 _dragStartPos;
        private PlayerMovementMulti _playerMovement;
        private PlayerLineRenderer _lineRenderer;
        private bool _isDragging;

        private void Start()
        {
            Debug.Log($"HasInputAuthority: {HasInputAuthority}");
            _playerMovement = GetComponent<PlayerMovementMulti>();
            _lineRenderer = GetComponent<PlayerLineRenderer>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out NetworkInputData inputData))
            {
                Debug.LogError("No input data received.");
                return;
            }

            if (inputData.touchState == NetworkInputData.TouchState.Began)
            {
                DragStart(inputData.touchPos);
            }
            if (_isDragging && inputData.touchState == NetworkInputData.TouchState.Moved)
            {
                Dragging(inputData.touchPos);
            }
            if (_isDragging && inputData.touchState == NetworkInputData.TouchState.Ended)
            {
                DragRelease(inputData.touchPos);
            }
        }

        private void DragStart(Vector3 touchPos)
        {
            _dragStartPos = transform.position;
            _lineRenderer.StartLine(_dragStartPos);
            _isDragging = true;
        }

        private void Dragging(Vector3 touchPos)
        {
            _lineRenderer.UpdateLine(touchPos);
        }

        private void DragRelease(Vector3 touchPos)
        {
            _isDragging = false;
            _lineRenderer.ClearLine();
            Vector3 dragVector = touchPos - _dragStartPos;
            Vector3 clampedDrag = Vector3.ClampMagnitude(dragVector, _playerMovement.maxDrag);

            RpcApplyForce(_dragStartPos, _dragStartPos + clampedDrag);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RpcApplyForce(Vector3 startPos, Vector3 endPos)
        {
            Debug.Log($"Applying force from {startPos} to {endPos}");
            _playerMovement.ApplyForce(startPos, endPos);
        }
    }
}