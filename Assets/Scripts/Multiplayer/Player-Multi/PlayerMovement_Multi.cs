using Fusion;
using UnityEngine;

namespace Multiplayer.Player_Multi
{
    public class PlayerMovementMulti : NetworkBehaviour
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
            float forceStrength = Mathf.Clamp(force.magnitude, 0f, maxDrag); // Ogranicz długość siły
            Vector3 clampedForce = force.normalized * forceStrength * power;

            rb.AddForce(clampedForce, ForceMode2D.Impulse);

            if (Object.HasStateAuthority)
            {
                NetworkedPosition = rb.position;
            }
        }


        private void FixedUpdate()
        {
            if (!PlayerMulti.isReady) return;
            if (Object.HasStateAuthority)
            {
                NetworkedPosition = rb.position;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, NetworkedPosition, 0.1f);
            }
        }
    }
}