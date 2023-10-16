
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
        ClampToWindow();
    }

    //void LateUpdate() => ClampToWindow();

    void ClampToWindow()
    {
        Vector3 pos = moveRectTransform.localPosition;

        Vector3 minPosition = moveRectTransform.rect.min - moveRectTransform.rect.min;
        Vector3 maxPosition = moveRectTransform.rect.max - moveRectTransform.rect.max;

        pos.x = Mathf.Clamp(moveRectTransform.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(moveRectTransform.localPosition.y, minPosition.y, maxPosition.y);

        moveRectTransform.localPosition = pos;
    }

}
