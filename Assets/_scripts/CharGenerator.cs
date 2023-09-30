using Fusion;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharGen
{
    public string name;
    public string desc;
    public Texture2D portrait;
}

public class CharGenerator : MonoBehaviour
{
    public List<CharGen> charList = new();
    public List<Texture2D> portList = new();
    public int portIndex = 0;

    //[Networked] public int SelectedTextureIndex { get; set; }

    public static CharGenerator ins;
    public int localPlayerId;

    private void Awake()
    {
        ins = this;
    }

    public Texture2D GetMaterial()
    {
        return portList[portIndex];
    }
    public Texture2D GetTexture(int index)
    {
        return portList[index];
    }
    //public override void Spawned()
    //{
    //    SelectedTextureIndex = portIndex;
    //}
}
