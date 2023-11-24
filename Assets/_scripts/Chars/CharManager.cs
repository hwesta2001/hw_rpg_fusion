using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharManager : MonoBehaviour
{
    public CharList PLAYER_CHARNW;
    public void SetChar(ref CharNW charNW)
    {
        PLAYER_CHARNW._charNW = charNW;
        PLAYER_CHARNW._playerid = charNW.playerID;
    }


    public List<CharList> CHARNW_LIST;

    public CharNW GetChar(int id)
    {
        foreach (var item in CHARNW_LIST)
        {
            if (item._playerid == id)
            {
                return item._charNW;
            }
            else
            {
                return new CharNW();
            }
        }
        return new CharNW();
    }
    public void AddList(PlayerRef _player, CharNW charNW)
    {
        CHARNW_LIST.Add(new CharList(_player, charNW));
        SetCharIcons();
    }

    public void RemoveList(PlayerRef _player)
    {
        for (int i = 0; i < CHARNW_LIST.Count; i++)
        {
            if (CHARNW_LIST[i]._playerid == _player)
            {
                CHARNW_LIST.RemoveAt(i);
                break;
            }
        }
        SetCharIcons();
    }

    public void SetTurnEndReady(int playerid, bool ready)
    {
        for (int i = 0; i < CHARNW_LIST.Count; i++)
        {
            if (CHARNW_LIST[i]._playerid == playerid)
            {
                CHARNW_LIST[i]._charNW.isTurnReady = ready;
                break;
            }
        }
    }

    public bool IsAllCharsReadyToTurn()
    {
        bool allTrue = true;
        if (CHARNW_LIST.Count <= 0) allTrue = false;
        for (int i = 0; i < CHARNW_LIST.Count; i++)
        {
            if (CHARNW_LIST[i]._charNW.isTurnReady == false)
            {
                allTrue = false;
                break;
            }
        }
        return allTrue;
    }

    void SetCharIcons()
    {
        CharIconControl.ins.DisableAll();

        for (int i = 0; i < CHARNW_LIST.Count; i++)
        {
            CharIconControl.ins.CharIconSet(CHARNW_LIST[i]._charNW);
        }


        //foreach (CharNW item in CHARNW_LIST)
        //{
        //    CharIconControl.ins.CharIconSet(item);
        //}
    }



    #region PortraitSetters-Done-
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
            //Destroy(charGetandSet, .5f);
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
    #endregion
}



[System.Serializable]
public class CharList
{
    public int _playerid;
    public CharNW _charNW;

    public CharList(int playerid, CharNW charNW)
    {
        _playerid = playerid;
        _charNW = charNW;
    }
}