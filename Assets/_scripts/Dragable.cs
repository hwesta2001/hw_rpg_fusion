
//Put this an ui element want to clicked
//add moveRectTransform
//And it moves the moveRectTransform

using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    [SerializeField] RectTransform moveRectTransform;

    private void OnEnable()
    {
        canvas = GetComponentInParent<Canvas>();
    }
    private void Start()
    {
        if (moveRectTransform == null) Debug.LogWarning(" moveRectTransform missing");
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        moveRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


}
