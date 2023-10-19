using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkController : MonoBehaviour
{
    public PlayerNetworkStart thisRunner;
    public PlayerRef thisPlayer;
    public PLAYER _player;

    private void OnEnable()
    {
        thisRunner = FindFirstObjectByType<PlayerNetworkStart>();
        thisPlayer = thisRunner.thisPlayer;
        var items = FindObjectsOfType<PLAYER>();
        foreach (PLAYER item in items)
        {
            if (thisPlayer.PlayerId == item.CHAR_NW.playerID)
            {
                _player = item;
                return;
            }
        }
    }
}
