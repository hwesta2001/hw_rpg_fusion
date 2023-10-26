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
            SetCharIcon();        //CharNW ye g�re icon yeniden ayarlan�r. Stats de�i�imde falan
            SetHealthValue();     //CharNW health ile char health iconu de�i�ir.
        }
    }

    void SetCharIcon()
    {
        charNameText.text = Char_NW.name.ToString();
        charPortraitImage.texture = CharManager.ins.GetTexture(Char_NW.portraitId);
    }

    public void SetHealthValue()
    {
        charHealthSlider.maxValue = (float)Char_NW.MaxHealth;
        charHealthSlider.value = (float)Char_NW.CurrentHealth;
    }

    public void OpenCharSheet_Button()
    {
        CharSheetControl.ins.OpenCharSheet(charNW);
    }
}