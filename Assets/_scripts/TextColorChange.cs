using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Color initColor;
    [SerializeField] Color targetColor = Color.green;

    private void OnValidate()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.color = initColor;
    }
    public void ChangeColor()
    {
        textMeshProUGUI.color = GetComponentInParent<Toggle>().isOn ? targetColor : initColor;
    }
}
