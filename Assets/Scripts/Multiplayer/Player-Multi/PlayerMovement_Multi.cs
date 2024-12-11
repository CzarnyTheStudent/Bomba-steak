using Fusion;
using Player;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerMovementMulti : NetworkBehaviour
    {
        public float power = 10f;
        public float maxDrag = 5f;
        public Rigidbody2D rb;
        public Vector3 startPos;
        private Rigidbody2D
            _rigidbody =
                null;

        [Networked] private Vector3 NetworkedPosition { get; set; }
        
        public override void Spawned()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            if (Object.HasInputAuthority)
            {
                startPos = transform.position;
            }
        }


        public void ResetPos()
        {
            if (Object.HasInputAuthority)
            {
                transform.position = startPos;
            }
        }

        
        public override void FixedUpdateNetwork()
        {
       
        }

        public void ApplyForce(Vector3 startPos, Vector3 endPos)
        {
            Vector3 force = startPos - endPos;
            float forceStrength = Mathf.Clamp(force.magnitude, 0f, maxDrag);
            Vector3 clampedForce = force.normalized * forceStrength * power;

            _rigidbody.AddForce(clampedForce, ForceMode2D.Impulse);
        }

    }
}