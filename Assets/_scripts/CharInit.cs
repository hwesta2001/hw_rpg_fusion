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

    //[Networked(OnChanged = nameof(SpawnInit))] public NetworkBool On_Spawned { get; set; }

    //public static void SpawnInit(Changed<CharInit> changed)
    //{
    //    // spawn olduktan sonraki metotlarý buraya yaz.



    //    changed.Behaviour.Rend.material.mainTexture = CharGenerator.ins.GetMaterial();

    //}

    public override void Spawned()
    {
        if (HasStateAuthority == false) return;
        Rend.material.mainTexture = CharGenerator.ins.GetMaterial();
    }
}


//using Fusion;
//using UnityEngine;

//public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
//{
//    public GameObject PlayerPrefab;

//    public void PlayerJoined(PlayerRef player)
//    {
//        if (player == Runner.LocalPlayer)
//        {
//            Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player);
//        }
//    }
//}