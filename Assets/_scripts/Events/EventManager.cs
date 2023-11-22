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
            AlertText.ins.AddText("Rolled " + DiceControl.ins.RolledDice + " for event determine", Color.magenta);
            AlertText.ins.AddText("Event Invoked", Color.cyan);
            Invoke(nameof(DebugReturnWaitState), 3f);  // for debug
            // Invoke Event:::
        }
    }


    void DebugReturnWaitState()
    {
        Turn.ins.TURN_STATE = TurnState.waiting;
    }

}
