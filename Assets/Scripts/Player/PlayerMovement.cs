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

        public override void Spawned()
        {
            if (Object.HasStateAuthority)
                startPos = transform.position;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RpcApplyForce(Vector3 startPos, Vector3 endPos)
        {
            Vector3 force = startPos - endPos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
            rb.AddForce(clampedForce, ForceMode2D.Impulse);
        }

        public void ResetPos()
        {
            if (Object.HasStateAuthority)
                transform.position = startPos;
        }
    }
}