using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharIcon : MonoBehaviour
{
    private CharNW charNW;
    [SerializeField] TextMeshProUGUI charNameText;
    [SerializeField] RawImage charPortraitImage;
    [SerializeField] Slider charHealthSlider;

    public CharNW Char_NW
    {
        get => charNW; set
        {
            charNW = value;
            SetCharIcon();
        }
    }

    public void SetCharIcon()
    {
        charNameText.text = Char_NW.name.ToString();
        charPortraitImage.texture = CharManager.ins.GetTexture(Char_NW.portraitId);
        SetHealthValue();
    }

    public void SetHealthValue()
    {
        charHealthSlider.maxValue = (float)Char_NW.MaxHealth;
        charHealthSlider.value = (float)Char_NW.CurrentHealth;
    }
}