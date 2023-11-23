using UnityEngine;
using Fusion;

// playerLeft ile -save player, char, quest, position vb, yapabiliriz.
public class PLAYER : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] Transform cameraFollow;
    RunnerStart pNetworkStart;
    Renderer _rend;
    Renderer Rend
    {
        get
        {
            if (_rend == null)
                _rend = GetComponentInChildren<Renderer>();
            return _rend;
        }
    }
    [Networked(OnChanged = nameof(OnCharNwCompleted))] NetworkBool CharNwCompleted { get; set; }
    public static void OnCharNwCompleted(Changed<PLAYER> changed)
    {
        changed.Behaviour.gameObject.name = changed.Behaviour.CHAR_NW.name.ToString() + "_" + changed.Behaviour.CHAR_NW.playerID;
        //CharIconControl.ins.CharIconSet(changed.Behaviour.CHAR_NW);
        CharManager.ins.AddList(changed.Behaviour.CHAR_NW.playerID, changed.Behaviour.CHAR_NW);
        if (!changed.Behaviour.HasInputAuthority) return;
        CharManager.ins.SetChar(ref changed.Behaviour.CHAR_NW);
    }

    [Networked(OnChanged = nameof(OnCharChanged))] public ref CharNW CHAR_NW => ref MakeRef<CharNW>();
    protected static void OnCharChanged(Changed<PLAYER> changed)
    {
        //changed.Behaviour.gameObject.name = changed.Behaviour.CHAR_NW.name.ToString() + "_" + changed.Behaviour.CHAR_NW.playerID;
        //CharIconControl.ins.CharIconSet(changed.Behaviour.CHAR_NW);
    }

    [Networked(OnChanged = nameof(OnMatIndexChanged))] public int MatIndex { get; set; }
    public static void OnMatIndexChanged(Changed<PLAYER> changed)
    {
        Material mat = changed.Behaviour.Rend.material;
        mat.mainTexture = CharManager.ins.GetTexture(changed.Behaviour.MatIndex);
        changed.Behaviour.Rend.material = mat;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority) return;
        if (player == Runner.LocalPlayer)
        {
            pNetworkStart = FindFirstObjectByType<RunnerStart>();
            MatIndex = pNetworkStart.nPortIndex;
            CHAR_NW.playerID = (byte)player.PlayerId;
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
        Debug.LogWarning(player.ToString() + " ... player left the game!");
        CharManager.ins.RemoveList(player);
        if (pNetworkStart._spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            if (!networkObject.HasStateAuthority) return;
            if (player != Runner.LocalPlayer) return;


            Debug.Log(" Despawning " + networkObject.Name);
            Runner.Despawn(networkObject);
            pNetworkStart._spawnedCharacters.Remove(player);
            //CharIconControl.ins.CharIconRemove(CHAR_NW);
        }
    }

    void Despawned()
    {
        Debug.Log("Depawned this object and called Despawned method");
    }
}
