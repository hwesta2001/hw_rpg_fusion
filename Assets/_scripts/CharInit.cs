using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharInit : NetworkBehaviour
{
    Renderer _rend;
    Renderer Rend
    {
        get
        {
            if (_rend == null)
                _rend = GetComponentInChildren<Renderer>();
            return _rend;
        }
    }

    [Networked(OnChanged = nameof(SpawnInit))] public int MatIndex { get; set; }
    [Networked/*(OnChanged = nameof(SpawnInit))*/] public NetworkBool On_Spawned { get; set; }

    public static void SpawnInit(Changed<CharInit> changed)
    {
        // spawn olduktan sonraki metotlarý buraya yaz.
        if (!changed.Behaviour.HasStateAuthority) return;
        Material mat = changed.Behaviour.Rend.material;
        mat.mainTexture = CharGenerator.ins.GetTexture(changed.Behaviour.MatIndex);
        changed.Behaviour.Rend.material = mat;
    }

    public override void Spawned()
    {
        base.Spawned();
        if (!HasStateAuthority) return;
        On_Spawned = !On_Spawned;
    }
}
