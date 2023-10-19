using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using Hw.Dice;

public class DiceNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [field: SerializeField]
    [Networked(OnChanged = nameof(OnDiceRolled))] public byte DICE_ROLL { get; set; }

    protected static void OnDiceRolled(Changed<DiceNetwork> changed)
    {
        DiceControl.ins.RolledDice = changed.Behaviour.DICE_ROLL;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (HasStateAuthority == false) return;
        DiceControl.OnRollDice += RollRandom;
    }

    public void PlayerLeft(PlayerRef player)
    {
        DiceControl.OnRollDice -= RollRandom;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RollDiceRPC()
    {
        DiceControl.ins.Roll_Dice(DICE_ROLL);
    }

    void RollRandom()
    {
        if (HasStateAuthority == false) return;
        if (DiceControl.ins.RollingNow()) return;
        DICE_ROLL = (byte)UnityEngine.Random.Range(1, DiceControl.ins.DiceFaceCount + 1);
        RollDiceRPC();
    }
}
