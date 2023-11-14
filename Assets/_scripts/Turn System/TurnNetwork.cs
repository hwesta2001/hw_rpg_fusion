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
            //DebugText.ins.AddText("All Ready To Turn End..Turn state :  " + TurnState.moveStart);
            if (Turn.ins.TURN_STATE == TurnState.events)
            {
                Debug.Log("Event state is passed");
            }
            Turn.ins.TURN_STATE = TurnState.moveStart;
        }
        else
        {
            if (Turn.ins.TURN_STATE == TurnState.events)
            {
                Debug.Log("Event state is on");
                return;
            }
            Turn.ins.TURN_STATE = TurnState.waiting;
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        turnCanvas.SetActive(true);
        Turn.ins.TURN_COUNT = 0;
        Turn.OnTurnChanged += TurnChaned;
        readyToggle.isOn = false;
        TurnEnd();
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        Turn.ins.TURN_COUNT = 0;
        Turn.OnTurnChanged -= TurnChaned;
        readyToggle.isOn = false;
        TurnEnd();
    }

    void TurnChaned(TurnState ts)
    {
        if (HasStateAuthority)
        {
            if (ts == TurnState.events)
            {
                readyToggle.isOn = false;
            }
        }
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
