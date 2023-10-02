using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Editor;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;
using System.Linq;

public class PlayerNetworkStart : MonoBehaviour, INetworkRunnerCallbacks
{
    #region Implementations

    //public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner) { }
    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    #endregion

    [SerializeField] NetworkRunner _runner;
    [SerializeField] NetworkPrefabRef _playerPrefab;
    public Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();
    [SerializeField] List<Transform> _spawnPoints = new();

    [Space, Tooltip("Canvas Controlers")]
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] TMP_InputField ifield;
    [SerializeField] GameObject deletedObject;

    [Networked] public int nPortIndex { get; set; }

    private void Awake()
    {
        _runner = gameObject.GetComponent<NetworkRunner>();
        _runner.ProvideInput = true;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => _runner.IsConnectedToServer);
        Debug.LogWarning(_runner.SessionInfo.Name);
    }

    public void StartClient()
    {
        StartGame(GameMode.Shared, ifield.text);
        ifield.interactable = false;
        ifield.pointSize = 9.5f;
        ifield.image.color = new Color(0, 0, 0, 0);
        buttonCanvas.SetActive(false);
        Destroy(deletedObject);
    }

    async void StartGame(GameMode mode, string gameID)
    {
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
        if (player == runner.LocalPlayer)
        {
            //Create a unique position for the player
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, Vector3.up * 10000, Quaternion.identity, player);
            networkPlayerObject.transform.position = _spawnPoints[player.PlayerId % _spawnPoints.Count].position;
            networkPlayerObject.transform.name = "PLAYER_" + player.PlayerId.ToString();
            _spawnedCharacters.Add(player, networkPlayerObject);

            DebugText.ins.AddText(networkPlayerObject.name + " HasStateAuthority: " + networkPlayerObject.HasStateAuthority);
            CharGenerator.ins.localPlayerId = player.PlayerId;
            nPortIndex = CharGenerator.ins.portIndex;
        }
    }

    public void On_PlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        //// Find and remove the players avatar

        //if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        //{
        //    Debug.Log(" Depawning " + networkObject.Name);
        //    if (!networkObject.HasStateAuthority) return;
        //    if (player != runner.LocalPlayer) return;
        //    runner.Despawn(networkObject);
        //    _spawnedCharacters.Remove(player);
        //}
    }

}
