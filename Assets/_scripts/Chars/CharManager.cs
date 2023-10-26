using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
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

            //kullanýlmayan scriptleri kapatalým
            //GetComponent<CharGenPortSelect>().enabled = false;
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
