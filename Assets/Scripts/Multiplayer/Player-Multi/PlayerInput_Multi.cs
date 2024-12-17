using Fusion;
using Player;
using System.Collections;
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
            if (!Object.HasInputAuthority) return;
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
            if (!shootReady) return;
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

        [SerializeField] private float cooldownTime = 1.5f;
        public bool shootReady = true;
        public IEnumerator WaitForShoot()
        {
            shootReady = false;
            yield return new WaitForSeconds(cooldownTime);
            shootReady = true;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcApplyForce(Vector3 startPos, Vector3 endPos)
        {
            _playerMovement.ApplyForce(startPos, endPos);
        }
    }
}