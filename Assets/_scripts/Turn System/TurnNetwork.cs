using Fusion;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{

    [SerializeField] Toggle readyToggle;
    [SerializeField] GameObject turnCanvas;
    [field: SerializeField][Networked(OnChanged = nameof(OnTurn_Count_Changed))] public NetworkBool Turn_Count { get; set; }
    protected static void OnTurn_Count_Changed(Changed<TurnNetwork> changed)
    {
        if (Turn.ins.TURN_STATE == TurnState.waiting)
        {
            CharManager.ins.SetTurnEndReady(changed.Behaviour.Turn_Count);
        }
        else
        {
            CharManager.ins.SetTurnEndReady(true);
        }
        if (!changed.Behaviour.HasStateAuthority) return;
        if (CharManager.ins.IsAllCharsReadyToTurn())
        {
            Turn.ins.TURN_STATE = TurnState.moveStart;
        }

        //Old way
        //if (Turn.ins.TURN_COUNT >= changed.Behaviour.Runner.ActivePlayers.Count())
        //{
        //    if (Turn.ins.TURN_STATE == TurnState.events)
        //    {
        //        Debug.Log("Event state is passed");
        //    }
        //    Turn.ins.TURN_STATE = TurnState.moveStart;
        //}
        //else
        //{
        //    if (Turn.ins.TURN_STATE == TurnState.events)
        //    {
        //        Debug.Log("Event state is on");
        //        return;
        //    }
        //    Turn.ins.TURN_STATE = TurnState.waiting;
        //}

    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        turnCanvas.SetActive(true);
        //Turn.ins.TURN_COUNT = 0;
        Turn.OnTurnChanged += TurnChaned;
        readyToggle.isOn = false;
        TurnEnd();
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        //Turn.ins.TURN_COUNT = 0;
        Turn.OnTurnChanged -= TurnChaned;
        readyToggle.isOn = false;
        TurnEnd();
    }

    void TurnChaned(TurnState ts)
    {
        if (HasStateAuthority)
        {
            switch (ts)
            {
                case TurnState.waiting:
                    readyToggle.isOn = false;
                    readyToggle.gameObject.SetActive(true);
                    break;
                case TurnState.moveStart:
                    readyToggle.gameObject.SetActive(false);
                    break;
                case TurnState.moving:
                    readyToggle.gameObject.SetActive(false);
                    break;
                case TurnState.events:
                    readyToggle.gameObject.SetActive(false);
                    break;
                case TurnState.invokeEvent:
                    readyToggle.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public void TurnEnd() // readyToggle da onValueChange de ekli
    {
        if (!HasStateAuthority) return;
        Turn_Count = readyToggle.isOn;
    }
}
