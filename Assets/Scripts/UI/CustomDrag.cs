using UnityEngine.EventSystems;
using UnityEngine;

public class CustomDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject objectToInteractWith;
    private ICustomDrag onDrag;

    void Start()
    {
        onDrag = objectToInteractWith.GetComponent<ICustomDrag>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.OnCurrentDrag();
    }
}
