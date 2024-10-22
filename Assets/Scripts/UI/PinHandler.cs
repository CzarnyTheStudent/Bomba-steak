using GameTools;
using UnityEngine;

public class PinHandler : MonoBehaviour, ICustomDrag
{
    [SerializeField] private float maxPullDistance = 1.6f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private AudioClip pullSound;
    [SerializeField] private GameObject Handle;
    [SerializeField] private GameObject HandleStatic;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 previousTouchPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition; 
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = pullSound;
    }

    public void OnCurrentDrag()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 currentTouchPosition = touch.position;
            
            float deltaX = currentTouchPosition.x - previousTouchPosition.x;
            previousTouchPosition = currentTouchPosition;

 
            float newX = Mathf.Clamp(rectTransform.anchoredPosition.x + deltaX, startPosition.x, startPosition.x + maxPullDistance);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(newX, startPosition.y), Time.deltaTime * moveSpeed);
            
            Handle.SetActive(true);
            HandleStatic.SetActive(false);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
            if (rectTransform.anchoredPosition.x >= maxPullDistance)
            {
                PinFullyPulled(); 
            }
        }
    }

    private void PinFullyPulled()
    {
        rectTransform.anchoredPosition = startPosition;
        Handle.SetActive(false);
        HandleStatic.SetActive(true);
        GameManager.Instance.PinPulled();
    }
}
