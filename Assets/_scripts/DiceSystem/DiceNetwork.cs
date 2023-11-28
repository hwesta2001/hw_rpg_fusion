using UnityEngine;
using Fusion;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DiceNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] TextMeshProUGUI DiceTextOnPlayer;
    [SerializeField] GameObject diceRollonPlayerCanvas;
    [SerializeField] Image diceImage;
    [field: SerializeField]
    WaitForSeconds wfs;
    [Networked(OnChanged = nameof(OnDiceRolled))] public byte DICE_ROLL { get; set; }
    [Networked(OnChanged = nameof(OnDiceStringChanged))] public NetworkString<_4> RolledDiceText { get; set; }

    protected static void OnDiceRolled(Changed<DiceNetwork> changed)
    {
        DiceControl.ins.RolledDice = changed.Behaviour.DICE_ROLL;
        changed.Behaviour.StartCo_InvokeActiveDiceTextOnPlayer();
        changed.Behaviour.SetDiceTextOnPlayer(changed.Behaviour.DICE_ROLL.ToString());
    }

    protected static void OnDiceStringChanged(Changed<DiceNetwork> changed)
    {
        changed.Behaviour.DiceTextOnPlayer.text = changed.Behaviour.RolledDiceText.ToString();
    }

    public void SetDiceTextOnPlayer(string text)
    {
        if (HasStateAuthority == false) return;
        //DiceTextOnPlayer.text = text;
        RolledDiceText = text;
    }


    public void PlayerJoined(PlayerRef player)
    {
        if (HasStateAuthority == false) return;
        ActiveDiceTextOnPlayer(false);
        wfs = new(1f);
        DiceControl.OnRollDice += RollRandom;
        Turn.OnTurnChanged += ControlGameState;
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (HasStateAuthority == false) return;
        DiceControl.OnRollDice -= RollRandom;
        Turn.OnTurnChanged -= ControlGameState;
    }

    void StartCo_InvokeActiveDiceTextOnPlayer()
    {
        if (HasStateAuthority == false) return;
        StartCoroutine(InvokeActiveDiceTextOnPlayer());
    }
    IEnumerator InvokeActiveDiceTextOnPlayer()
    {
        yield return wfs;
        ActiveDiceTextOnPlayer(true);
    }

    public void ActiveDiceTextOnPlayer(bool isactive)
    {
        if (HasStateAuthority == false) return;
        diceRollonPlayerCanvas.SetActive(isactive);
    }


    void RollRandom()
    {
        if (HasStateAuthority == false) return;
        if (DiceControl.ins.RollingNow()) return;
        DICE_ROLL = (byte)UnityEngine.Random.Range(1, DiceControl.ins.DiceFaceCount + 1);
        RollDiceRPC();
    }
    // with RPC all clients rolling
    //[Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RollDiceRPC()
    {
        if (HasStateAuthority == false) return;
        DiceControl.ins.Roll_Dice(DICE_ROLL);
    }

    Color defColor = new(35, 21, 71);
    void ControlGameState(TurnState ts)
    {
        switch (ts)
        {
            case TurnState.waiting:
            case TurnState.events:
            case TurnState.invokeEvent:
                diceImage.color = defColor;
                break;
            case TurnState.moveStart:
            case TurnState.moving:
                diceImage.color = Color.green;
                break;
            default:
                break;
        }
    }
}
