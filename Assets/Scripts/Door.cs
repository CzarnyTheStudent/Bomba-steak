using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 targetOffset; // Przesunięcie względem obecnej pozycji
    public float moveSpeed = 5f;
    public AudioSource doorSound;

    private bool shouldMove;
    private Vector2 startPosition;
    private Vector2 endPosition;

    public void StartMovement()
    {
        if (doorSound != null)
        {
            doorSound.Play(); 
        }
        
        shouldMove = true;
        startPosition = transform.position;
        endPosition = startPosition + targetOffset;
    }

    private void Update()
    {
        if (shouldMove)
        {
            Vector2 currentPosition = transform.position;
            float newX = Mathf.MoveTowards(currentPosition.x, endPosition.x, moveSpeed * Time.deltaTime);
            float newY = Mathf.MoveTowards(currentPosition.y, endPosition.y, moveSpeed * Time.deltaTime);

            transform.position = new Vector3(newX, newY, transform.position.z);
            
            if (Mathf.Approximately(newX, endPosition.x) && Mathf.Approximately(newY, endPosition.y))
            {
                shouldMove = false;
            }
        }
    }
}