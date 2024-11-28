using Multiplayer.Player_Multi;
using UnityEngine;

namespace Player
{
    public class PlayerLineRenderer : MonoBehaviour
    {
        public LineRenderer lr;
        private PlayerMovementMulti _playerMovement;
        private bool drag;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovementMulti>();
        }
        
        private void Update()
        {
            if (!drag) return;
            lr.SetPosition(0, transform.position);
        }
            
        public void StartLine(Vector3 touchPos)
        {
            lr.positionCount = 1;
            lr.SetPosition(0, touchPos);
            drag = true;
        }

        public void UpdateLine(Vector3 currentPos)
        {
            Vector3 dragVector = currentPos - lr.GetPosition(0);
            float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, _playerMovement.maxDrag); 
            Vector3 clampedEndPos = lr.GetPosition(0) + dragVector.normalized * dragDistance;

            lr.positionCount = 2;
            lr.SetPosition(1, clampedEndPos);
        }


        public void ClearLine()
        {
            lr.positionCount = 0;
            drag = false;
        }
    }
}