using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharSheetSet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI charName_Text;
    [SerializeField] TextMeshProUGUI raceClass_Text;
    [SerializeField] TextMeshProUGUI stats_Text;
    [SerializeField] TextMeshProUGUI desc_Text;
    [SerializeField] RawImage portrait_Image;
    [SerializeField] RectTransform thisRectTransform;
    public byte _playerId;


    public void SetSheet(CharNW charNW)
    {
        thisRectTransform.anchoredPosition = new(20f + 5 * charNW.playerID, -65f);
        charName_Text.text = charNW.name.ToString();
        raceClass_Text.text = charNW.race.ToString() + "  " + charNW.classes.ToString();
        stats_Text.text = "Strenght: " + charNW.strength.ToString() + "\n" +
                          "Agility: " + charNW.agility.ToString() + "\n" +
                          "Intelligent: " + charNW.intelligence.ToString() + "\n" +
                          "Charisma: " + charNW.charisma.ToString() + "\n" +
                          "Luck: " + charNW.luck.ToString();

        desc_Text.text = charNW.desc.ToString();
        portrait_Image.texture = CharManager.ins.portList[charNW.portraitId];
        _playerId = charNW.playerID;
    }
}
