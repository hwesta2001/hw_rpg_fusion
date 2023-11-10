using Fusion;
using Hw.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnNetwork : NetworkBehaviour
{
    
    public int sharedInt;

    // Her istemcinin kendi sahnesindeki nesneye eriþebilmesi için bir singleton örneði
    public static TurnNetwork Instance { get; private set; }

    // Nesne baþlatýldýðýnda, singleton örneðini ayarla ve sharedInt deðerini 0 olarak baþlat
    private void Awake()
    {
        Instance = this;
    }




    //[Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void TurnEnd()
    {
        Turn.TURN_COUNT++;
    }
}
