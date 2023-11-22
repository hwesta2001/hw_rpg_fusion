using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class InitNetwork : SimulationBehaviour, IPlayerJoined
{
    public List<GameObject> spawnedNetworkObjects = new();
    public List<GameObject> AwakedObjects = new();
    public List<GameObject> activatedObjects = new();
    public List<GameObject> deactivatedObjects = new();
    public List<GameObject> deletedObjects = new();

    //private IEnumerator Start()
    //{
    //    yield return new WaitUntil(() => GameState.CurrentState == GameStates.Connected);

    //}

    private void Awake()
    {
        foreach (var obj in AwakedObjects)
        {
            obj.SetActive(true);
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        InitAll();
        if (player != Runner.LocalPlayer) return;
        foreach (var obj in spawnedNetworkObjects)
        {
            Runner.Spawn(obj, new Vector3(0, 1, 0), Quaternion.identity, player);
        }
        this.enabled = false;
        DebugText.ins.AddText("Init Network");
    }

    private void InitAll()
    {
        foreach (var obj in activatedObjects)
        {
            obj.SetActive(true);
        }
        foreach (var obj in deactivatedObjects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in deletedObjects)
        {
            Destroy(obj, 1f);
        }
    }
}
