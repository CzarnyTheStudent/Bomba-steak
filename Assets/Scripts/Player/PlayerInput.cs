using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 _dragStartPos;
        private bool _isDragging = false;
        private PlayerMovement _playerMovement;
        private PlayerLineRenderer _lineRenderer;
        private PlayerStats _playerStats;
    
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _lineRenderer = GetComponent<PlayerLineRenderer>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0f;

                if (touch.phase == TouchPhase.Began && IsTouchOnObject(touchPos))
                {
                    DragStart(touchPos);
                }
                if (_isDragging && touch.phase == TouchPhase.Moved)
                {
                    Dragging(touchPos);
                }
                if (_isDragging && touch.phase == TouchPhase.Ended)
                {
                    DragRelease(touchPos);
                }
            }
        }

        private bool IsTouchOnObject(Vector3 touchPos)
        {
            Collider2D collider = GetComponent<Collider2D>();
            return collider == Physics2D.OverlapPoint(touchPos);
        }

        private void DragStart(Vector3 touchPos)
        {
            _isDragging = true;
            _dragStartPos = touchPos;
            _lineRenderer.StartLine(_dragStartPos);
        }

        private void Dragging(Vector3 touchPos)
        {
            _lineRenderer.UpdateLine(touchPos);
        }

        private void DragRelease(Vector3 touchPos)
        {
            _isDragging = false;
            _playerStats.IncrementDragEndCount();
            _lineRenderer.ClearLine();
            _playerMovement.ApplyForce(_dragStartPos, touchPos);
        }
    }
}