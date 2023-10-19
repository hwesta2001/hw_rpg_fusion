using Hw.Dice;
using UnityEngine;
using Fusion;
using System;

public class DiceControl : MonoBehaviour
{
    #region Singelton
    public static DiceControl ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    public byte RolledDice;
    public byte DiceFaceCount;
    [SerializeField] GameObject[] allDices = new GameObject[6];
    DiceRoll diceRoll;

    [SerializeField] Dice currentDice;
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

    public static Action OnRollDice;
    public void RollButton() { OnRollDice?.Invoke(); }
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
    }
}