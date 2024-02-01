using UnityEngine;
using Fusion;

// playerLeft ile -save player, char, quest, position vb, yapabiliriz.
public class PLAYER : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] Transform cameraFollow;
    RunnerStart pNetworkStart;
    Renderer _rend;
    Renderer Rend {
        get {
            if (_rend == null)
                _rend = GetComponentInChildren<Renderer>();
            return _rend;
        }
    }

    ChangeDetector _changeDetector;
    [Networked] NetworkBool CharNwCompleted { get; set; }
    [Networked] public ref CharNW CHAR_NW => ref MakeRef<CharNW>();
    [Networked] public int MatIndex { get; set; }


    public void OnCharNwCompleted()
    {
        gameObject.name = $"PLAYER_{CHAR_NW.playerID} - {CHAR_NW.name}";
        //CharIconControl.ins.CharIconSet(changed.Behaviour.CHAR_NW);
        CharManager.ins.AddList(Runner.LocalPlayer, CHAR_NW);
        if (!HasInputAuthority) return;
        CharManager.ins.SetChar(ref CHAR_NW);
    }

    protected void OnCharChanged()
    {
        //gameObject.name = CHAR_NW.name.ToString() + "_" + CHAR_NW.playerID;
        //CharIconControl.ins.CharIconSet(CHAR_NW);
    }

    public void OnMatIndexChanged()
    {
        Material mat = Rend.material;
        mat.mainTexture = CharManager.ins.GetTexture(MatIndex);
        Rend.material = mat;
    }

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }


    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this)) {
            switch (change) {
                case nameof(CharNwCompleted):
                    OnCharNwCompleted();
                    break;
                case nameof(CHAR_NW):
                    OnCharChanged();
                    break;
                case nameof(MatIndex):
                    OnMatIndexChanged();
                    break;
            }
        }
    }


    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        if (player == Runner.LocalPlayer) {
            pNetworkStart = FindFirstObjectByType<RunnerStart>();
            MatIndex = pNetworkStart.nPortIndex;
            CHAR_NW.playerID = player.PlayerId;
            CHAR_NW.name = CharManager.ins.PLAYER_CHAR.name;
            CHAR_NW.race = CharManager.ins.PLAYER_CHAR.race;
            CHAR_NW.classes = CharManager.ins.PLAYER_CHAR.classes;
            CHAR_NW.desc = CharManager.ins.PLAYER_CHAR.desc;
            CHAR_NW.portraitId = CharManager.ins.PLAYER_CHAR.portraitId;
            CHAR_NW.strength = CharManager.ins.PLAYER_CHAR.strength;
            CHAR_NW.agility = CharManager.ins.PLAYER_CHAR.agility;
            CHAR_NW.intelligence = CharManager.ins.PLAYER_CHAR.intelligence;
            CHAR_NW.charisma = CharManager.ins.PLAYER_CHAR.charisma;
            CHAR_NW.luck = CharManager.ins.PLAYER_CHAR.luck;

            // health sistemi sonraso deðiþecek
            CHAR_NW.MaxHealth = 100;
            CHAR_NW.CurrentHealth = UnityEngine.Random.Range(80, 101);

            //CameraControl.ins.SetTarget(cameraFollow);
            CameraControlOrbit.ins.SetTarget(cameraFollow);

            CharNwCompleted = !CharNwCompleted;

        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        string V = "Player_" + player.ToString() + " - '" + player.PlayerId.GetChar().name + "' left the game!";
        V.ToLog();
        Debug.LogWarning(V);
        CharManager.ins.RemoveList(player);
        if (pNetworkStart._spawnedCharacters.TryGetValue(player, out NetworkObject networkObject)) {
            if (!networkObject.HasStateAuthority) return;
            if (player != Runner.LocalPlayer) return;
            Runner.Despawn(networkObject);
            pNetworkStart._spawnedCharacters.Remove(player);
        }
    }


    void Despawned()
    {
        Debug.Log("Depawned this object and called Despawned method");
    }
}
