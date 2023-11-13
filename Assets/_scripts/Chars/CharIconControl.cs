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

    public void DisableAll()
    {
        foreach (var item in char_icon_list)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void CharIconRemove(int _playerID)
    {
        char_icon_list[_playerID].gameObject.SetActive(false);
        DebugText.ins.AddText("Char iconremove???   " + _playerID);
    }

    public void CharIconReload(CharNW _charNW)
    {
        //CharNW ye göre icon yeniden ayarlanýr. Stats deðiþimde falan
        //CharNW health ile char health iconu deðiþir.
        char_icon_list[_charNW.playerID].Char_NW = _charNW;
    }
}
