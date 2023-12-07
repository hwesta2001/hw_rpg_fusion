using Fusion;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemSlot : INetworkStruct
{
    public int itemid;
    public int stackSize;
    public ItemSlot(int itemId, int stackSize)
    {
        this.itemid = itemId;
        this.stackSize = stackSize;
    }
}

public class InventoryNetwork : NetworkBehaviour
{
    #region getInventory
    Inventory inventory { get; set; }
    void GetInvetory()
    {
        if (HasStateAuthority)
        {
            inventory = GetComponentInChildren<Inventory>();
        }
    }
    #endregion

    const byte invCap = 16;
    [Networked(OnChanged = nameof(OnInvChanged))]
    [Capacity(invCap)]
    [UnitySerializeField]
    public NetworkDictionary<int, ItemSlot> PLAYER_INVENTORY => default;
    // key is slotindex
    // value is itemSlot that contains itemid and itemStackSize data
    public static void OnInvChanged(Changed<InventoryNetwork> changed)
    {
        // ýnvetory changed!!!!
    }
    public override void Spawned()
    {
        GetInvetory();

    }

    public void AddItemToInventory(Item item)
    {
        for (int i = 0; i < invCap; i++)
        {
            // öncelikle bu item staklanýyor mu bak.
            if (item.stackSize > 1)
            {
                // evet stacklanýyor
                // bu item inventoryde var mý bak
                for (int x = 0; x < invCap; x++)
                {
                    if (item.itemId == PLAYER_INVENTORY.Get(x).itemid)
                    {
                        // evet var stack size 1 arttýr
                        int size = PLAYER_INVENTORY.Get(x).stackSize;
                        size++;
                        PLAYER_INVENTORY.Set(x, new(item.itemId, size));
                        PutItemToSlot(x, item, size);
                        break;
                    }
                    else
                    {
                        // inv bu item yok item ilk boþ slota yerleþtir ve stack size 1 yap
                        if (isAFreeInvSlot(out int index))
                        {
                            PutItemToSlot(index, item);
                        }
                    }
                }

            }
            else
            {
                // hayýr stacklanmýyor bos bir slota yerleþtir
                if (isAFreeInvSlot(out int index))
                {
                    PutItemToSlot(index, item);
                }
            }
        }
    }


    void PutItemToSlot(int slotIndex, Item item, int? stackSize = null)
    {
        if (slotIndex > invCap) return;
        inventory.AddToInventory(slotIndex, item, stackSize ?? 1);
    }

    bool isAFreeInvSlot(out int _index)
    {
        if (inventory.freeSlotAvaliable(out int index))
        {
            _index = index;
            return true;
        }
        else
        {
            AlertText.ins.AddText("Inventory is full", _color: Color.red);
            _index = 100;
            return false;
        }
    }
}
