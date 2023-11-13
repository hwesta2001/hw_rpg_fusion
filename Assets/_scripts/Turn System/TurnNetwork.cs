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
        Turn.ins.TURN_COUNT += changed.Behaviour.Turn_Count;
        if (Turn.ins.TURN_COUNT < 0) Turn.ins.TURN_COUNT = 0;

        if (Turn.ins.TURN_COUNT >= changed.Behaviour.Runner.ActivePlayers.Count())
        {
            DebugText.ins.AddText("All Ready To Turn End..Turn state :  " + TurnState.moveStart);
            Turn.ins.TURN_STATE = TurnState.moveStart;
        }
        else
        {
            DebugText.ins.AddText("Not Ready To End............. ");
            Turn.ins.TURN_STATE = TurnState.waiting;
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        turnCanvas.SetActive(true);
        Turn.ins.TURN_COUNT = 0;
        readyToggle.isOn = false;
        TurnEnd();
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        Turn.ins.TURN_COUNT = 0;
        readyToggle.isOn = false;
        TurnEnd();
    }

    public void TurnEnd() // readyToggle da onValueChange de ekli
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
