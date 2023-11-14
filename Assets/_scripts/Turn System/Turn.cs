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
            DebugText.ins.AddText("TurnState is " + turnState.ToString());
            OnTurnChanged?.Invoke(turnState);
        }
    }

    [field: SerializeField] public int TURN_COUNT { get; set; }


    public static Turn ins;
    private void Awake()
    {
        ins = this;
    }
}

public enum TurnState
{
    waiting, moveStart, moving, events
}