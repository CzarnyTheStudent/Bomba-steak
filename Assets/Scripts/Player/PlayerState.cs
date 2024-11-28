using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        private Rigidbody2D rb;
        private bool isMoving = false;
        private bool wasMoving = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            isMoving = rb.velocity.magnitude > 0.1f;
        }

        public bool IsPlayerMoving()
        {
            return isMoving;
        }

        public bool DidMovementChange()
        {
            bool movementChanged = isMoving != wasMoving;
            wasMoving = isMoving;
            return movementChanged;
        }
    }
}