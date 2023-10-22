using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameState : SimulationBehaviour
{
    public static GameStates CurrentState;

    [SerializeField, Tooltip("Dont Change in Inspector it is a static var, DEBUG ONLY")]
    GameStates cState;
    public static GameState instance;

    void Awake()
    {
        CurrentState = GameStates.Init;
        instance = this;
    }

    void Update()
    {
        if (cState != CurrentState)
        {
            cState = CurrentState;
            StateChanged(cState);
        }
    }

    void StateChanged(GameStates c_State)
    {
        switch (c_State)
        {
            case GameStates.Init:
                DebugText.ins.AddText("Game State is " + c_State.ToString());
                break;
            case GameStates.Connected:
                DebugText.ins.AddText("Game State is " + c_State.ToString());
                break;
            case GameStates.InGameLoop:
                DebugText.ins.AddText("Game State is " + c_State.ToString());
                break;
            case GameStates.Waiting:
                DebugText.ins.AddText("Game State is " + c_State.ToString());
                break;
            case GameStates.Lost:
                DebugText.ins.AddText("Game State is " + c_State.ToString());
                break;
            default:
                break;
        }
    }


}
