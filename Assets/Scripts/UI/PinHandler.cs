using GameTools;
using UnityEngine;

public class PinHandler : MonoBehaviour, ICustomDrag
{
    [SerializeField] private float maxPullDistance = 200f;
    [SerializeField] private float moveSpeed = 20f;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private bool pinPulled = false;
    private Vector2 previousTouchPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition; 
    }

    public void OnCurrentDrag()
    {
        if (pinPulled) return; 

        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            Vector2 currentTouchPosition = touch.position;

            
            float deltaX = currentTouchPosition.x - previousTouchPosition.x;


            previousTouchPosition = currentTouchPosition;

 
            float newX = Mathf.Clamp(rectTransform.anchoredPosition.x + deltaX, startPosition.x, startPosition.x + maxPullDistance);

           
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(newX, startPosition.y), Time.deltaTime * moveSpeed);

            Debug.Log(rectTransform.anchoredPosition);
            float final = startPosition.x + maxPullDistance;
            if (rectTransform.position.x >= final)
            {
                PinFullyPulled();
            }
        }
    }

    private void PinFullyPulled()
    {
        pinPulled = true;
        Debug.Log("Zawleczka zosta³a wyci¹gniêta!");
        GameManager.Instance.PinPulled();
    }
}
