using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class TurnNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] int playerID;
    [SerializeField] Toggle readyToggle;
    [SerializeField] GameObject turnCanvas;
    NetworkObject networkObject;
    public void PlayerJoined(PlayerRef player)
    {
        if (HasStateAuthority)
        {
            networkObject = GetBehaviour<NetworkObject>();
            turnCanvas.SetActive(true);
            playerID = player.PlayerId;
            Turn.OnTurnChanged += TurnChaned;
            readyToggle.isOn = false;
            Rpc_TurnControl();
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
        Rpc_TurnControl();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_TurnControl()
    {
        AlertText.ins.AddText(" Rpc_TurnControl _ " + playerID);
        if (Turn.ins.TURN_STATE == TurnState.waiting)
        {
            CharManager.ins.SetTurnEndReady(playerID, readyToggle.isOn);
            if (HasStateAuthority)
            {
                if (CharManager.ins.IsAllCharsReadyToTurn())
                {
                    Debug.LogWarning("RCP Called");
                    Turn.ins.TURN_STATE = TurnState.moveStart;
                    return;
                }
            }
        }
        else
        {
            CharManager.ins.SetTurnEndReady(playerID, true);
        }
    }
}
