using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharInit : NetworkBehaviour
{
    private Renderer _rend;
    Renderer Rend
    {
        get
        {
            if (_rend == null)
                _rend = GetComponentInChildren<Renderer>();
            return _rend;
        }
    }

    [Networked(OnChanged = nameof(RendChanged))] public NetworkBool On_Spawned { get; set; }

    public static void RendChanged(Changed<CharInit> changed)
    {

        changed.Behaviour.Rend.material.mainTexture = CharGenerator.ins.GetMaterial();
    }

    public override void Spawned()
    {
        On_Spawned = !On_Spawned;
    }
}
