using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNetwork : NetworkBehaviour
{
    #region singleton
    public static InventoryNetwork Ins { get; private set; }
    void SingletonInit()
    {
        if (HasStateAuthority)
        {
            Ins = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion


    [Networked(OnChanged = nameof(OnInvChanged)), Capacity(25), UnitySerializeField]
    public NetworkDictionary<int, Item> PLAYER_INVENTORY => default;
    public static void OnInvChanged(Changed<InventoryNetwork> changed)
    {
        // ýnvetory changed!!!!
    }
    public override void Spawned()
    {
        SingletonInit();
    }

    public void AddItemToInventory(int id)
    {

    }
}
