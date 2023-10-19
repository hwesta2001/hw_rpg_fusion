using UnityEngine;
using Fusion;
using System;

// playerLeft ile -save player, char, quest, position vb, yapabiliriz.
public class PLAYER : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    PlayerNetworkStart pNetworkStart;
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




    [Networked(OnChanged = nameof(OnCharChanged))] public ref CharNW CHAR_NW => ref MakeRef<CharNW>();
    protected static void OnCharChanged(Changed<PLAYER> changed)
    {
        changed.Behaviour.gameObject.name = changed.Behaviour.CHAR_NW.name.ToString() + "_" + changed.Behaviour.CHAR_NW.playerID;
    }

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
            pNetworkStart = FindFirstObjectByType<PlayerNetworkStart>();
            MatIndex = pNetworkStart.nPortIndex;
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
        if (pNetworkStart._spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            Debug.Log(" Despawning " + networkObject.Name);
            if (!networkObject.HasStateAuthority) return;
            if (player != Runner.LocalPlayer) return;
            Runner.Despawn(networkObject);
            pNetworkStart._spawnedCharacters.Remove(player);
        }
    }
}
