using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetPosition : MonoBehaviour
{
    [SerializeField] Vector3 pos;
    [SerializeField] RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        pos = rectTransform.anchoredPosition;
    }
    private void OnEnable()
    {
        rectTransform.anchoredPosition = pos;
    }

    [ContextMenu("SetPos")]
    public void SetPos() // buttonda
    {
        rectTransform.anchoredPosition = pos;
    }
}
