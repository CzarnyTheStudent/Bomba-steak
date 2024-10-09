using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    private Vector3 dragStartPos;
    private bool isDragging = false;
    private int dragEndCount = 0;

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
            if (isDragging && touch.phase == TouchPhase.Moved)
            {
                Dragging(touchPos);
            }
            if (isDragging && touch.phase == TouchPhase.Ended)
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
        isDragging = true;
        dragStartPos = touchPos;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }

    private void Dragging(Vector3 touchPos)
    {
        lr.positionCount = 2;
        lr.SetPosition(1, touchPos);
    }

    private void DragRelease(Vector3 touchPos)
    {
        isDragging = false;
        dragEndCount++; 
        GameDataStatsReceiver.Instance.ReceiveDragEndCount(dragEndCount);
        lr.positionCount = 0;

        Vector3 force = dragStartPos - touchPos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }
}