using System;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public static Action<TurnState> OnTurnChanged { get; set; }
    [SerializeField] TurnState turnState;
    public TurnState TURN_STATE
    {
        get
        {
            return turnState;
        }
        set
        {
            turnState = value;
        }
    }

    public static Turn ins;
    private void Awake()
    {
        ins = this;
    }
    private int turnCount;
    public int TURN_COUNT
    {
        get => turnCount;
        set
        {
            turnCount = value;
            DebugText.ins.AddText("TurnCount:" + TURN_COUNT);
        }
    }
}

public enum TurnState
{
    waiting, moveStart, moving, _event
}