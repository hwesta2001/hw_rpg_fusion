using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class GameState : NetworkBehaviour
{
    public static GameStates CurrentState;

    [SerializeField, Tooltip("Dont Change in Inspector it is a static var, DEBUG ONLY")]
    GameStates cState;

    public List<byte> TurnIDs = new();

    public static GameState ins;



    //public override void Spawned()
    //{
    //    if (!HasStateAuthority) return;
    //    base.Spawned();
    //    TurnIDs.Clear();
    //    var players = FindObjectsOfType<PLAYER>();

    //    foreach (var player in players)
    //    {
    //        if (player != null)
    //        {
    //            TurnIDs.Add(player.CHAR_NW.playerID);
    //        }
    //    }
    //}


    void Awake()
    {
        CurrentState = GameStates.Init;
        ins = this;
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
                Debug.Log("Game State is " + c_State.ToString());
                break;
            case GameStates.Connected:
                Debug.Log("Game State is " + c_State.ToString());
                break;
            case GameStates.InGameLoop:
                Debug.Log("Game State is " + c_State.ToString());
                break;
            case GameStates.Waiting:
                Debug.Log("Game State is " + c_State.ToString());
                break;
            case GameStates.Lost:
                Debug.Log("Game State is " + c_State.ToString());
                break;
            default:
                break;
        }
    }


}
