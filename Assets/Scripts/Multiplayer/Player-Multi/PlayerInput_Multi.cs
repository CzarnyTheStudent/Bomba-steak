using Fusion;
using Player;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerInputMulti : NetworkBehaviour
    {
        private Vector3 _dragStartPos;
        private PlayerMulti _playerMulti;
        private PlayerMovementMulti _playerMovement;
        private PlayerLineRenderer _lineRenderer;
        private PlayerDataNetworked _netData;
        private bool _isDragging;

        public override void Spawned()
        {
            // --- Host & Client
            // Set the local runtime references.
            _playerMulti = GetComponent<PlayerMulti>();
            _playerMovement = GetComponent<PlayerMovementMulti>();
            _lineRenderer = GetComponent<PlayerLineRenderer>();
            _netData = GetComponent<PlayerDataNetworked>();

            // --- Host
            // The Game Session SPECIFIC settings are initialized
        }

        public override void FixedUpdateNetwork()
        {
            if (!_playerMulti.AcceptInput) return;
            if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var input))
            {
                ProcessTouchInput(input);
            }
        }
        
        private void ProcessTouchInput(NetworkInputData inputData)
        {
            if (inputData.touchState == NetworkInputData.TouchState.Began)
            {
                DragStart(inputData.touchPos);
            }
            else if (_isDragging && inputData.touchState == NetworkInputData.TouchState.Moved)
            {
                Dragging(inputData.touchPos);
            }
            else if (_isDragging && inputData.touchState == NetworkInputData.TouchState.Ended)
            {
                DragRelease(inputData.touchPos);
                _netData.AddDragToCount();
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

            if (!Object.HasInputAuthority) return;
            RpcApplyForce(_dragStartPos, _dragStartPos + clampedDrag);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RpcApplyForce(Vector3 startPos, Vector3 endPos)
        {
            _playerMovement.ApplyForce(startPos, endPos);
        }
    }
}