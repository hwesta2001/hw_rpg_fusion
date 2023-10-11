using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Net;

public class PLAYER : NetworkBehaviour, IPlayerJoined, IPlayerLeft // playerLeft ile -save player, char, quest, position vb, yapabiliriz.
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

    [Networked] public ref CharNW CHAR_NW => ref MakeRef<CharNW>();

    [Networked(OnChanged = nameof(OnMatIndexChanged))] public int MatIndex { get; set; }
    public static void OnMatIndexChanged(Changed<PLAYER> changed)
    {
        Material mat = changed.Behaviour.Rend.material;
        mat.mainTexture = CharManager.ins.GetTexture(changed.Behaviour.MatIndex);
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
            CHAR_NW.playerID = (byte)player.PlayerId;
            CHAR_NW.name = CharManager.ins.PLAYER_CHAR.name;
            CHAR_NW.race = CharManager.ins.PLAYER_CHAR.race;
            CHAR_NW.classes = CharManager.ins.PLAYER_CHAR.classes;
            CHAR_NW.desc = CharManager.ins.PLAYER_CHAR.desc;
            CHAR_NW.portraitId = CharManager.ins.PLAYER_CHAR.portraitId;
            CHAR_NW.strength = CharManager.ins.PLAYER_CHAR.strength;
            CHAR_NW.agility = CharManager.ins.PLAYER_CHAR.agility;
            CHAR_NW.intelligence = CharManager.ins.PLAYER_CHAR.intelligence;
            CHAR_NW.charisma = CharManager.ins.PLAYER_CHAR.charisma;
            CHAR_NW.luck = CharManager.ins.PLAYER_CHAR.luck;

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