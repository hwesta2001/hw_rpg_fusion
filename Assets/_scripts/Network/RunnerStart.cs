using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RunnerStart : MonoBehaviour, INetworkRunnerCallbacks
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

    public NetworkRunner _runner;
    [SerializeField] NetworkPrefabRef _playerPrefab;
    [field: SerializeField]
    public Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();
    [SerializeField] List<Transform> _spawnPoints = new();

    [Space, Tooltip("Canvas Controlers")]
    [SerializeField] GameObject joinButton;
    [SerializeField] TMP_InputField joinInputField;
    [SerializeField] List<GameObject> deletedObjects = new();

    [Networked] public int nPortIndex { get; set; }
    public PlayerRef thisPlayer;
    private void Awake()
    {
        _runner = gameObject.GetComponent<NetworkRunner>();
        _runner.ProvideInput = true;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => _runner.IsConnectedToServer);
        Debug.LogWarning(_runner.SessionInfo.Name);
        AlertText.ins.AddText(_runner.SessionInfo.Name + " season started", Color.magenta);
    }


    // PLAYER BURDA SPAWM OLUYOR StartClient()
    public void StartClient()
    {
        if (!FindFirstObjectByType<Toggle>().isOn)
        {
            DebugText.ins.AddText(" Your are not ready, set a char and click ready");
            return;
        }
        StartGame(GameMode.Shared, joinInputField.text);
        SetChangedObjectsAfterJoin();
        CharManager.ins.GenerateChar();
    }

    void SetChangedObjectsAfterJoin()
    {
        joinInputField.interactable = false;
        joinInputField.pointSize = 15f;
        //joinInputField.GetComponent<RectTransform>().localScale = new(.6f, .6f, 1);
        joinInputField.textComponent.alignment = TextAlignmentOptions.BottomRight;
        joinInputField.image.color = new Color(0, 0, 0, 0);

        joinButton.SetActive(false);
        foreach (var item in deletedObjects)
        {
            Destroy(item);
        }
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
            thisPlayer = player;
            //Create a unique position for the player
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, Vector3.up * 10000, Quaternion.identity, player);
            networkPlayerObject.transform.position = _spawnPoints[player.PlayerId % _spawnPoints.Count].position;
            _spawnedCharacters.Add(player, networkPlayerObject);

            DebugText.ins.AddText(CharManager.ins.PLAYER_CHAR.name + " Spawned " + networkPlayerObject.HasStateAuthority);
            nPortIndex = CharManager.ins.PortIndex;
            GameState.CurrentState = GameStates.Connected;
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
