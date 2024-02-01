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

    ChangeDetector _changeDetector;
    [Networked] public byte DICE_ROLL { get; set; }
    [Networked] public NetworkString<_4> RolledDiceText { get; set; }
    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }

    protected void OnDiceRolled()
    {
        DiceControl.ins.RolledDice = DICE_ROLL;
        StartCo_InvokeActiveDiceTextOnPlayer();
        SetDiceTextOnPlayer(DICE_ROLL.ToString());
    }

    protected void OnDiceStringChanged()
    {
        DiceTextOnPlayer.text = RolledDiceText.ToString();
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(RolledDiceText):
                    OnDiceStringChanged();
                    break;
                case nameof(DICE_ROLL):
                    OnDiceRolled();
                    break;
            }
        }
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
