using Hw.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum Dice { D4, D6, D8, D10, D12, D20 }

public class DiceControl : MonoBehaviour
{
    [field: SerializeField] public byte RolledDice { get; private set; }

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

    [ContextMenu("Roll Dice")]
    public void RollDice()
    {
        if (diceRoll == null) SetDice();
        if (currentDice != CurrentDice) SetDice();
        diceRoll.RollDice();
        RolledDice = (byte)diceRoll.RolledDice;
    }

    [ContextMenu("Switch Dice")]
    void SetDice()
    {
        CurrentDice = currentDice;
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
                allDices[0].SetActive(true);
                diceRoll = allDices[0].GetComponent<DiceRoll>();
                break;
            case Dice.D6:
                allDices[1].SetActive(true);
                diceRoll = allDices[1].GetComponent<DiceRoll>();
                break;
            case Dice.D8:
                allDices[2].SetActive(true);
                diceRoll = allDices[2].GetComponent<DiceRoll>();
                break;
            case Dice.D10:
                allDices[3].SetActive(true);
                diceRoll = allDices[3].GetComponent<DiceRoll>();
                break;
            case Dice.D12:
                allDices[4].SetActive(true);
                diceRoll = allDices[4].GetComponent<DiceRoll>();
                break;
            case Dice.D20:
                allDices[5].SetActive(true);
                diceRoll = allDices[5].GetComponent<DiceRoll>();
                break;
            default:
                break;
        }
    }
}