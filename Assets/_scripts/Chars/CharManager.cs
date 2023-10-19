using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
    public Chars PLAYER_CHAR { get; private set; }

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

            //kullanılmayan scriptleri kapatalım
            GetComponent<CharGenPortSelect>().enabled = false;
            charGetandSet.enabled = false;
        }
    }

    public Texture2D GetTexture(int index)
    {
        return portList[index];
    }

    Sprite GetRawImage(int index) // kullanılmıyor suan
    {
        return Sprite.Create(GetTexture(index), new Rect(0, 0, GetTexture(index).width, GetTexture(index).height), new Vector2(0.5f, 0.5f));
    }

    Sprite GetSprite(Texture2D GetText) // kullanılmıyor suan
    {
        return Sprite.Create(GetText, new Rect(0, 0, GetText.width, GetText.height), new Vector2(0.5f, 0.5f));
    }
}
