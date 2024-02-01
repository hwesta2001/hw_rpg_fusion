using UnityEngine;
using Fusion;
using System;

public class GameState : SimulationBehaviour
{


    public static GameState Ins;
    public Action<GameStates> OnGameStateChanged;

    [SerializeField, Tooltip("Dont Change in Inspector it is a static var, DEBUG ONLY")]
    private GameStates currentState;
    public GameStates CurrentState // THIS IS MAIN GAME STATE
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
            StateChanged(currentState);
        }
    }

    void Awake()
    {
        CurrentState = GameStates.Init;
        Ins = this;
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

        OnGameStateChanged?.Invoke(c_State);
    }


}
