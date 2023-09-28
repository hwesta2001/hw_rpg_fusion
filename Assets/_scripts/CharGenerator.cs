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

public class CharGenerator : NetworkBehaviour
{
    public List<CharGen> charList = new();
    public List<Texture2D> portList = new();
    public int portIndex = 0;

    [Networked] public int SelectedTextureIndex { get; set; }

    public static CharGenerator ins;


    private void Awake()
    {
        ins = this;
    }

    public Texture2D GetMaterial()
    {
        return portList[SelectedTextureIndex];
    }

    public override void Spawned()
    {
        SelectedTextureIndex = portIndex;
        DebugText.ins.AddText("selected texture index: " + SelectedTextureIndex);
    }

}
