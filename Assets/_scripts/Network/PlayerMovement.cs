using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour/*, INetworkInput*/
{
    [SerializeField] byte turnId;

    public override void Spawned()
    {
        base.Spawned();
        turnId = GetComponent<PLAYER>().CHAR_NW.playerID;
    }

}
