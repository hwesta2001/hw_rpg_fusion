using Fusion;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{

    [SerializeField] Toggle readyToggle;
    [SerializeField] GameObject turnCanvas;
    [field: SerializeField][Networked(OnChanged = nameof(OnTurn_Count_Changed))] public int Turn_Count { get; set; }
    protected static void OnTurn_Count_Changed(Changed<TurnNetwork> changed)
    {
        Turn.TURN_COUNT += changed.Behaviour.Turn_Count;
        if (Turn.TURN_COUNT < 0) Turn.TURN_COUNT = 0;
        DebugText.ins.AddText("ActivePlayers.Count.. " + changed.Behaviour.Runner.ActivePlayers.Count());
        if (Turn.TURN_COUNT >= changed.Behaviour.Runner.ActivePlayers.Count())
        {
            DebugText.ins.AddText("All Ready To Turn End.............. ");
        }
        else
        {
            DebugText.ins.AddText("Not Ready To End............. ");
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        turnCanvas.SetActive(true);
        Turn.TURN_COUNT = 0;
        readyToggle.isOn = false;
        TurnEnd();
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        Turn.TURN_COUNT = 0;
        readyToggle.isOn = false;
        TurnEnd();
    }

    //[Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void TurnEnd()
    {
        if (!HasStateAuthority) return;
        if (readyToggle.isOn)
        {
            Turn_Count = 1;
        }
        else
        {
            Turn_Count = -1;
        }
    }
}
