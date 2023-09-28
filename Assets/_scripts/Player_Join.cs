using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Editor;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Player_Join : MonoBehaviour, INetworkRunnerCallbacks
{


    #region Implementations
    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }



    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    #endregion

    [SerializeField] NetworkRunner _runner;
    Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();
    [Space]
    [SerializeField] NetworkPrefabRef _playerPrefab;
    [SerializeField] List<Transform> spawnPoint = new();
    [SerializeField] int spanwPointIndex;
    [Space]
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] TMP_InputField ifield;
    [Space]
    [SerializeField] GameObject deletedObject;

    private void Awake()
    {
        _runner = gameObject.GetComponent<NetworkRunner>();
        _runner.ProvideInput = true;
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => _runner.IsCloudReady);
        Debug.LogWarning(_runner.SessionInfo.Name);
    }

    public void StartClient()
    {
        StartGame(GameMode.AutoHostOrClient, ifield.text);
        ifield.interactable = false;
        ifield.pointSize = 9.5f;
        ifield.image.color = new Color(0, 0, 0, 0);
        buttonCanvas.SetActive(false);
        Destroy(deletedObject);
    }

    async void StartGame(GameMode mode, string gameID)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        //_runner = gameObject.GetComponent<NetworkRunner>();

        _runner.ProvideInput = true;
        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = gameID,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>()
        });
    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {

            // Create a unique position for the player
            Vector3 spawnPosition = spawnPoint[spanwPointIndex % spawnPoint.Count].position;
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);

            //Debug.Log("Player connected: " + player.PlayerId);
            //Debug.Log("season id: " + runner.SessionInfo.Name);
            DebugText.ins.AddText("Player connected: " + player.PlayerId);
            spanwPointIndex++;
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }
}
