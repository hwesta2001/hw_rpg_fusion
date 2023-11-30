using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class TurnNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] int playerID;
    [SerializeField] Toggle readyToggle;
    [SerializeField] GameObject turnCanvas;
    //bool canClickReady;
    public void PlayerJoined(PlayerRef player)
    {
        if (HasStateAuthority)
        {
            //networkObject = GetBehaviour<NetworkObject>();
            turnCanvas.SetActive(true);
            playerID = player.PlayerId;
            Turn.OnTurnChanged += TurnChaned;
            readyToggle.isOn = false;
            TurnEnd();
            readyToggle.gameObject.SetActive(false);
            Invoke(nameof(ReadyToggleButtonOpen), 2.5f);
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (HasStateAuthority)
        {
            Turn.OnTurnChanged -= TurnChaned;
            readyToggle.isOn = false;
        }
    }

    void TurnChaned(TurnState ts)
    {
        if (HasStateAuthority)
        {
            switch (ts)
            {
                case TurnState.waiting:
                    readyToggle.isOn = false;
                    //canClickReady = true;
                    readyToggle.interactable = true;
                    Invoke(nameof(ReadyToggleButtonOpen), .5f);
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

    void ReadyToggleButtonOpen()
    {
        readyToggle.gameObject.SetActive(true);
    }

    public void TurnEnd() // readyToggle da onValueChange de ekli
    {
        if (!HasStateAuthority) return;
        //if (!canClickReady) return;
        Rpc_TurnControl();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_TurnControl()
    {
        if (Turn.ins.TURN_STATE == TurnState.waiting)
        {
            CharManager.ins.SetTurnEndReady(playerID, readyToggle.isOn);
            //DebugText.ins.AddText(" Rpc_TurnControl _ " + playerID + " is waiting state and readyToogle is " + readyToggle.isOn);
            if (HasStateAuthority)
            {
                if (CharManager.ins.IsAllCharsReadyToTurn())
                {
                    //canClickReady = false;
                    readyToggle.interactable = false;
                    Invoke(nameof(ReadyToTurn), .5f);
                    return;
                }
            }
        }
        else
        {
            DebugText.ins.AddText(" Rpc_TurnControl _ " + playerID + " is NOT waiting state and SetTurnEndReady(playerID, true), rt:" + readyToggle.isOn);
            CharManager.ins.SetTurnEndReady(playerID, true);
        }
    }

    void ReadyToTurn()
    {
        AlertText.ins.AddText("RCP Called - IsAllCharsReadyToTurn  P_" + playerID.GetChar().name);
        Debug.LogWarning("RCP Called - IsAllCharsReadyToTurn  player_" + playerID);
        Turn.ins.TURN_STATE = TurnState.moveStart;
    }
}
