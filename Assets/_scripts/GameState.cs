using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Init,
    Connected,
    InGameLoop,
    Waiting,
    Lost
}

public class GameState : MonoBehaviour
{
    public static GameStates CurrentState;
    [SerializeField, Tooltip("Dont Change Inspector it is a static var")] GameStates cState;
    [SerializeField] List<GameObject> EnableAfterConnected = new();

    void Awake()
    {
        CurrentState = GameStates.Init;
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
        if (c_State == GameStates.Connected)
        {
            foreach (var item in EnableAfterConnected)
            {
                item.SetActive(true);
            }
        }
    }


}
