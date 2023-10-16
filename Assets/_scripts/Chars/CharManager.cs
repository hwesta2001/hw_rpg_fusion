using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
    public Chars PLAYER_CHAR { get; private set; }
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
                //charGet.SetPortraitOnCharSheet(spriteList[portIndex]);
            }
        }
    }

    [Tooltip("Bu liste charlarý save edince olusacak. charlarý kydet yükle yaplýlýnca burdan char selecti ile oyuna girilebilir.")]
    public List<Chars> SelecableCharList = new();
    public List<Texture2D> portList = new();
    //public List<Sprite> spriteList = new();
    [SerializeField] int portIndex = 0;
    public int localPlayerId;

    public static CharManager ins;

    private void Awake()
    {
        ins = this;
    }
    //private void Start()
    //{
    //    foreach (var item in portList)
    //    {
    //        spriteList.Add(GetSprite(item));
    //    }
    //}

    public void GenerateChar()
    {
        if (TryGetComponent<CharGetAndSet>(out var charGet))
        {
            charGet.SetChar();
            PLAYER_CHAR = charGet.GetChar();
            PLAYER_CHAR.portraitId = (byte)PortIndex;
        }
    }

    public Texture2D GetTexture(int index)
    {
        return portList[index];
    }

    Sprite GetRawImage(int index) // kullanýlmýyor suan
    {
        return Sprite.Create(GetTexture(index), new Rect(0, 0, GetTexture(index).width, GetTexture(index).height), new Vector2(0.5f, 0.5f));
    }

    Sprite GetSprite(Texture2D GetText) // kullanýlmýyor suan
    {
        return Sprite.Create(GetText, new Rect(0, 0, GetText.width, GetText.height), new Vector2(0.5f, 0.5f));
    }
}
