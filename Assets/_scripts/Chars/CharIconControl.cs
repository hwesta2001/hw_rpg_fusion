using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIconControl : MonoBehaviour
{
    [SerializeField] CharIcon[] char_icon_list;

    public static CharIconControl ins;
    private void OnEnable()
    {
        if (ins == null) { ins = this; }
    }

    public void CharIconSet(CharNW _charNW)
    {
        char_icon_list[_charNW.playerID].gameObject.SetActive(true);
        char_icon_list[_charNW.playerID].Char_NW = _charNW;
    }
    public void CharIconRemove(CharNW _charNW)
    {
        char_icon_list[_charNW.playerID].gameObject.SetActive(false);
    }
    public void CharHealthSet(CharNW _charNW)
    {
        char_icon_list[_charNW.playerID].Char_NW = _charNW;
        char_icon_list[_charNW.playerID].SetHealthValue();
    }

}
