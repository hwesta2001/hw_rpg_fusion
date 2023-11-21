using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static EventManager Ins { get; private set; }
    void Awake() => Ins = this;
    void Start() => Turn.OnTurnChanged += InvokeEvent;
    void OnDisable() => Turn.OnTurnChanged -= InvokeEvent;

    public void InvokeEvent(TurnState turnState)
    {
        if (Turn.ins.TURN_STATE == TurnState.invokeEvent)
        {
            Invoke(nameof(DebugReturnWaitState), 4f);  // for debug
            // Invoke Event:::
            DebugText.ins.AddText(" a Event Invoked");
        }
    }


    void DebugReturnWaitState()
    {
        Turn.ins.TURN_STATE = TurnState.waiting;
    }

}
