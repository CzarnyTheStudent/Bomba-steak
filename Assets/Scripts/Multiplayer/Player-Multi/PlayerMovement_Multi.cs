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
            Vector3 force = startPos - endPos; // Oblicz siłę jako różnicę pozycji
            float forceStrength = Mathf.Clamp(force.magnitude, 0f, maxDrag); // Ogranicz siłę
            Vector3 clampedForce = force.normalized * forceStrength * power; // Skaluj siłę

            rb.AddForce(clampedForce, ForceMode2D.Impulse); // Zastosuj siłę

            Debug.DrawRay(startPos, clampedForce, Color.green, 2f); // Debugowanie siły

            if (Object.HasStateAuthority)
            {
                NetworkedPosition = rb.position;
            }
        }



        public override void FixedUpdateNetwork()
        {
            if (!Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var inputData)) return;

            if (inputData.IsDragging)
            {
                ApplyForce(inputData.DragStart, inputData.DragEnd);
            }
        }

    }
}