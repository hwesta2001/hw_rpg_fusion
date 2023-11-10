using Fusion;
using Hw.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnNetwork : NetworkBehaviour
{
    
    public int sharedInt;

    // Her istemcinin kendi sahnesindeki nesneye eri�ebilmesi i�in bir singleton �rne�i
    public static TurnNetwork Instance { get; private set; }

    // Nesne ba�lat�ld���nda, singleton �rne�ini ayarla ve sharedInt de�erini 0 olarak ba�lat
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
