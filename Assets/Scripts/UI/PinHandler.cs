using GameTools;
using UnityEngine;

public class PinHandler : MonoBehaviour, ICustomDrag
{
    [SerializeField] private float maxPullDistance = 200f; 
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

            // Aktualizujemy poprzedni¹ pozycjê
            previousTouchPosition = currentTouchPosition;

            // Obliczamy now¹ pozycjê, dodaj¹c ró¿nicê do obecnej pozycji
            float newX = Mathf.Clamp(rectTransform.anchoredPosition.x + deltaX, startPosition.x, startPosition.x + maxPullDistance);

            // Przesuwamy zawleczkê tylko o wyliczon¹ ró¿nicê
            rectTransform.anchoredPosition += new Vector2(newX, startPosition.y);

            Debug.Log($"Nowa pozycja zawleczki: {rectTransform.anchoredPosition}");

            if (Mathf.Approximately(rectTransform.anchoredPosition.x, startPosition.x + maxPullDistance))
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
