using UnityEngine;
using Fusion;

namespace Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        public float power = 10f;
        public float maxDrag = 5f;
        public Rigidbody2D rb;
        public Vector3 startPos;

        [Networked] private Vector3 NetworkedPosition { get; set; }

        public override void Spawned()
        {
            if (Object.HasStateAuthority)
            {
                startPos = transform.position;
            }
        }

        public void ResetPos()
        {
            if (Object.HasStateAuthority)
            {
                transform.position = startPos;
            }
        }

        public void ApplyForce(Vector3 startPos, Vector3 endPos)
        {
            Vector3 force = startPos - endPos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
            rb.AddForce(clampedForce, ForceMode2D.Impulse);

            if (Object.HasStateAuthority)
            {
                NetworkedPosition = rb.position;
            }
        }

        private void FixedUpdate()
        {
            if (Object.HasStateAuthority)
            {
                // Synchronizuj pozycję
                NetworkedPosition = rb.position;
            }
            else
            {
                // Odtwarzaj pozycję na innych klientach
                transform.position = Vector3.Lerp(transform.position, NetworkedPosition, 0.1f);
            }
        }
    }
}