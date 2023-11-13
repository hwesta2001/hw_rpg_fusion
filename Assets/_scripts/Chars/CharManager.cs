using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharManager : MonoBehaviour
{
    #region CharIcon
    public CharNW?[] CharNWList = new CharNW?[6];
    public void AddList(PlayerRef _player, CharNW charNW)
    {
        CharNWList[_player.PlayerId] = charNW;
        SetCharIcons();
    }

    public void RemoveList(PlayerRef _player, CharNW charNW)
    {
        CharNWList[_player.PlayerId] = null;
        SetCharIcons();
    }

    void SetCharIcons()
    {
        CharIconControl.ins.DisableAll();
        foreach (var item in CharNWList)
        {
            if (item != null)
            {
                CharIconControl.ins.CharIconSet((CharNW)item);
            }
        }
    }

    #endregion

    public CharBeforeNetwork PLAYER_CHAR { get; private set; }

    int _port;
    [SerializeField] int portIndex = 0;
    public int PortIndex
    {
        get
        {
            return portIndex;
        }
        set
        {
            portIndex = value;

            if (TryGetComponent<CharGetAndSet>(out var charGet))
            {
                charGet.SetPortraitId(portIndex);
            }
        }
    }

    public List<Texture2D> portList = new();

    public static CharManager ins;
    private void Awake()
    {
        ins = this;
    }

    public void GenerateChar()
    {
        if (TryGetComponent<CharGetAndSet>(out var charGetandSet))
        {
            charGetandSet.SetChar();
            PLAYER_CHAR = charGetandSet.GetChar();
            PLAYER_CHAR.portraitId = (byte)PortIndex;
            charGetandSet.enabled = false;
        }
    }

    public Texture2D GetTexture(int index)
    {
        return portList[index];
    }

    public void NextTexture()
    {
        _port++;
        if (_port >= portList.Count) _port = 0;
        PortIndex = _port;
    }

    public void PrevTexture()
    {
        _port--;
        if (_port < 0) _port = portList.Count - 1;
        PortIndex = _port;
    }

    //Sprite GetRawImage(int index) // kullanýlmýyor suan
    //{
    //    return Sprite.Create(GetTexture(index), new Rect(0, 0, GetTexture(index).width, GetTexture(index).height), new Vector2(0.5f, 0.5f));
    //}

    //Sprite GetSprite(Texture2D GetText) // kullanýlmýyor suan
    //{
    //    return Sprite.Create(GetText, new Rect(0, 0, GetText.width, GetText.height), new Vector2(0.5f, 0.5f));
    //}
}
