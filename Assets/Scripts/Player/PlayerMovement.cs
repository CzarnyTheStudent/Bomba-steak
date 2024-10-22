using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float power = 10f;
        public float maxDrag = 5f;
        public Rigidbody2D rb;
        public Vector3 startPos;

        void Start() => startPos = transform.position;
        public void ApplyForce(Vector3 startPos, Vector3 endPos)
        {
            Vector3 force = startPos - endPos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
            rb.AddForce(clampedForce, ForceMode2D.Impulse);
        }

        public void ResetPos()
        {
            transform.position = startPos;
           
            
        }
   
    }
}