using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharManager : MonoBehaviour
{
    [SerializeField] CharNW PLAYER_CHARNW;
    public void SetChar(ref CharNW charNW) { PLAYER_CHARNW = charNW; }

    public List<CharNW> CHARNW_LIST = new(6);

    public void AddList(PlayerRef _player, CharNW charNW)
    {
        if (CHARNW_LIST.Contains(charNW)) return;
        CHARNW_LIST.Add(charNW);
        SetCharIcons();
    }

    public void RemoveList(PlayerRef _player, CharNW charNW)
    {
        if (CHARNW_LIST.Contains(charNW))
        {
            CHARNW_LIST.Remove(charNW);
        }
        SetCharIcons();
    }

    public void SetTurnEndReady(bool ready)
    {
        if (CHARNW_LIST.Contains(PLAYER_CHARNW))
        {

            PLAYER_CHARNW.isTurnReady = ready;
        }
    }

    public bool IsAllCharsReadyToTurn()
    {
        bool ready = true;
        foreach (var item in CHARNW_LIST)
        {
            if (!item.isTurnReady)
            {
                ready = false;
                break;
            }
        }

        return ready;
    }

    void SetCharIcons()
    {
        CharIconControl.ins.DisableAll();
        foreach (CharNW item in CHARNW_LIST)
        {
            CharIconControl.ins.CharIconSet(item);
        }
    }


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
}
