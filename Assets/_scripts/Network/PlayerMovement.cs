using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour/*, INetworkInput*/
{
    [Networked] public byte TurnId { get; private set; }
    PLAYER _Player;
    public override void Spawned()
    {
        if (!HasStateAuthority) return;
        base.Spawned();
        _Player = GetComponent<PLAYER>();
        TurnId = _Player.CHAR_NW.playerID;
    }

}
