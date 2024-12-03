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

        
        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority)
            {
                NetworkedPosition = rb.position;
            }
            else
            {
                rb.position = Vector3.Lerp(rb.position, NetworkedPosition, 0.1f);
            }
        }

        public void ApplyForce(Vector3 startPos, Vector3 endPos)
        {
            Vector3 force = startPos - endPos;
            float forceStrength = Mathf.Clamp(force.magnitude, 0f, maxDrag);
            Vector3 clampedForce = force.normalized * forceStrength * power;

            Debug.Log($"Applying force: {clampedForce}");

            rb.AddForce(clampedForce, ForceMode2D.Impulse);
            NetworkedPosition = rb.position;
        }

    }
}