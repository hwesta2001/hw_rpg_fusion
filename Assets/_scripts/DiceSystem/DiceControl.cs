using Hw.Dice;
using UnityEngine;
using System;
using DG.Tweening;

public class DiceControl : MonoBehaviour
{
    #region Singelton
    public static DiceControl ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    public byte RolledDice { get; set; }
    public byte DiceFaceCount;
    [SerializeField] Light diceLight;
    [SerializeField] GameObject[] allDices = new GameObject[6];
    DiceRoll diceRoll;
    [SerializeField] GameObject DiceCanvas;
    [SerializeField] Transform diceButton;
    [SerializeField] Dice currentDice;
    bool canRoll;
    public Dice CurrentDice
    {
        get
        {
            return currentDice;
        }

        set
        {
            currentDice = value;
            SwitchDice();
        }
    }

    public static Action OnRollDice { get; set; }
    public void RollButton()
    {
        if (!canRoll) return;
        if (Turn.ins.TURN_STATE == TurnState.moveStart || Turn.ins.TURN_STATE == TurnState.events)
        {
            OnRollDice?.Invoke();
        }
    }

    public void Roll_Dice(byte rolled)
    {
        if (diceRoll == null) SetDice();
        if (currentDice != CurrentDice) SetDice();
        diceRoll.RollDice(rolled);
    }
    public bool RollingNow() => diceRoll.rollingNow;

    void SetDice()
    {
        CurrentDice = currentDice;
    }

    void OnEnable()
    {
        SetDice();
        ControlGameState(Turn.ins.TURN_STATE);
        Turn.OnTurnChanged += ControlGameState;
    }
    private void OnDisable()
    {
        Turn.OnTurnChanged -= ControlGameState;
    }

    void ControlGameState(TurnState ts)
    {
        switch (ts)
        {
            case TurnState.waiting:
                //DiceCanvas.SetActive(false);
                SetActiveState(false);
                break;
            case TurnState.moveStart:
                CurrentDice = Dice.D6;
                ChangeDiceLightColor(Color.white);
                //DiceCanvas.SetActive(true);
                SetActiveState(true);
                break;
            case TurnState.moving:
                ChangeDiceLightColor(Color.green);
                break;
            case TurnState.events:
                CurrentDice = Dice.D20;
                ChangeDiceLightColor(Color.white);
                //DiceCanvas.SetActive(true);
                SetActiveState(true);
                break;
            case TurnState.invokeEvent:
                ChangeDiceLightColor(Color.yellow);
                break;
            default:
                break;
        }
    }
    [SerializeField] Vector3 smallButtonSize = new(0.1f, 0.1f, 0.1f);
    void SetActiveState(bool active)
    {
        canRoll = !active;
        if (active)
        {
            DiceCanvas.SetActive(active);
            diceButton.localScale = smallButtonSize;
            diceButton.DOScale(Vector3.one, .5f).SetEase(Ease.OutBounce).OnComplete(() => canRoll = active);
        }
        else
        {
            canRoll = active;
            diceButton.localScale = Vector3.one;
            diceButton.DOScale(smallButtonSize, 1.6f).SetEase(Ease.OutBounce).OnComplete(() => DiceCanvas.SetActive(active));
        }

    }

    void OnDiceRollFinieshed()
    {
        if (Turn.ins.TURN_STATE == TurnState.events)
        {
            Turn.ins.TURN_STATE = TurnState.invokeEvent;
        }
        else if (Turn.ins.TURN_STATE == TurnState.moveStart)
        {
            Turn.ins.TURN_STATE = TurnState.moving;
        }
    }

    void SwitchDice()
    {
        foreach (var dice in allDices)
        {
            dice.SetActive(false);
        }

        switch (CurrentDice)
        {
            case Dice.D4:
                DiceFaceCount = 4;
                allDices[0].SetActive(true);
                diceRoll = allDices[0].GetComponent<DiceRoll>();
                break;
            case Dice.D6:
                DiceFaceCount = 6;
                allDices[1].SetActive(true);
                diceRoll = allDices[1].GetComponent<DiceRoll>();
                break;
            case Dice.D8:
                DiceFaceCount = 8;
                allDices[2].SetActive(true);
                diceRoll = allDices[2].GetComponent<DiceRoll>();
                break;
            case Dice.D10:
                DiceFaceCount = 10;
                allDices[3].SetActive(true);
                diceRoll = allDices[3].GetComponent<DiceRoll>();
                break;
            case Dice.D12:
                DiceFaceCount = 12;
                allDices[4].SetActive(true);
                diceRoll = allDices[4].GetComponent<DiceRoll>();
                break;
            case Dice.D20:
                DiceFaceCount = 20;
                allDices[5].SetActive(true);
                diceRoll = allDices[5].GetComponent<DiceRoll>();
                break;
            default:
                break;
        }
        diceRoll.OnDiceRollAnimEnd = OnDiceRollFinieshed;
    }

    public void ChangeDiceLightColor(Color _color)
    {
        diceLight.color = _color;
    }
}