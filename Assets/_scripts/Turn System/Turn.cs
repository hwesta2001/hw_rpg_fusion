using UnityEngine;

public class Turn : MonoBehaviour
{
    private static int turnCount;
    public static int TURN_COUNT
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
    idle, move, _event
}