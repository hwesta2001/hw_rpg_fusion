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
    [SerializeField] Image portrait_Image;
    public byte _playerId;
    public void SetSheet(CharNW charNW)
    {
        charName_Text.text = charNW.name.ToString();
        raceClass_Text.text = charNW.race.ToString() + "  " + charNW.classes.ToString();
        stats_Text.text = "Strenght: " + charNW.strength.ToString() + "\n" +
                          "Agility: " + charNW.agility.ToString() + "\n" +
                          "Intelligent: " + charNW.intelligence.ToString() + "\n" +
                          "Charisma: " + charNW.charisma.ToString() + "\n" +
                          "Luck: " + charNW.luck.ToString() + "\n";

        desc_Text.text = charNW.desc.ToString();
        portrait_Image.sprite = CharManager.ins.spriteList[charNW.portraitId];
        _playerId = charNW.playerID;
    }
}
