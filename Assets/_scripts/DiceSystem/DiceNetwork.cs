using UnityEngine;
using Fusion;
using TMPro;
using System.Collections;

public class DiceNetwork : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] TextMeshProUGUI DiceTextOnPlayer;
    [SerializeField] GameObject diceRollonPlayerCanvas;
    [field: SerializeField]
    WaitForSeconds wfs;
    [Networked(OnChanged = nameof(OnDiceRolled))] public byte DICE_ROLL { get; set; }

    protected static void OnDiceRolled(Changed<DiceNetwork> changed)
    {
        DiceControl.ins.RolledDice = changed.Behaviour.DICE_ROLL;
        changed.Behaviour.StartCo_InvokeActiveDiceTextOnPlayer();
        changed.Behaviour.SetDiceTextOnPlayer(changed.Behaviour.DICE_ROLL.ToString());
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (HasStateAuthority == false) return;
        ActiveDiceTextOnPlayer(false);
        wfs = new(1f);
        DiceControl.OnRollDice += RollRandom;
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (HasStateAuthority == false) return;
        DiceControl.OnRollDice -= RollRandom;
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

    public void SetDiceTextOnPlayer(string text)
    {
        if (HasStateAuthority == false) return;
        DiceTextOnPlayer.text = text;
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
}
