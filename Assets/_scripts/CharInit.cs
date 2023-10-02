using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Net;

public class CharInit : NetworkBehaviour, IPlayerJoined, IPlayerLeft // playerLeft ile -save player, char, quest, position vb, yapabiliriz.
{
    PlayerNetworkStart pns;
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

    [Networked(OnChanged = nameof(OnMatIndexChanged))] public int MatIndex { get; set; }

    public static void OnMatIndexChanged(Changed<CharInit> changed)
    {
        Material mat = changed.Behaviour.Rend.material;
        mat.mainTexture = CharGenerator.ins.GetTexture(changed.Behaviour.MatIndex);
        changed.Behaviour.Rend.material = mat;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        if (player == Runner.LocalPlayer)
        {
            pns = FindFirstObjectByType<PlayerNetworkStart>();
            MatIndex = pns.nPortIndex;
            DebugText.ins.AddText("MatIndex:  " + MatIndex);
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (pns._spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            Debug.Log(" Depawning " + networkObject.Name);
            if (!networkObject.HasStateAuthority) return;
            if (player != Runner.LocalPlayer) return;
            Runner.Despawn(networkObject);
            pns._spawnedCharacters.Remove(player);
        }
    }
}
