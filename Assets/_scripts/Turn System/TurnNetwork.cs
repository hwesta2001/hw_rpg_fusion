using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class TurnNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] int playerID;
    [SerializeField] Toggle readyToggle;
    [SerializeField] GameObject turnCanvas;
    [field: SerializeField][Networked(OnChanged = nameof(OnTurn_Count_Changed))] public NetworkBool Turn_Count { get; set; }
    protected static void OnTurn_Count_Changed(Changed<TurnNetwork> changed)
    {
        if (Turn.ins.TURN_STATE == TurnState.waiting)
        {
            CharManager.ins.SetTurnEndReady(changed.Behaviour.playerID, changed.Behaviour.Turn_Count);
            print("111");
            if (CharManager.ins.IsAllCharsReadyToTurn())
            {
                Turn.ins.TURN_STATE = TurnState.moveStart;
                print("4444");
            }
        }
        else
        {
            CharManager.ins.SetTurnEndReady(changed.Behaviour.playerID, true);
            print("2222");
        }

    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        turnCanvas.SetActive(true);
        playerID = player.PlayerId;
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
                    readyToggle.isOn = false;
                    TurnEnd();
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
