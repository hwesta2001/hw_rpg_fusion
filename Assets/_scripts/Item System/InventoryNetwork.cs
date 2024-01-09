using Fusion;
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

    [Networked] public int PLAYER_GOLD { get; private set; }

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
        if (!HasStateAuthority) return;
        GetInvetory();
        RegisterItemAdderEvents(true);
    }
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if (!HasStateAuthority) return;
        RegisterItemAdderEvents(false);
    }

    void RegisterItemAdderEvents(bool add)
    {
        if (add)
        {
            DebugItemSystem.DebugItemAdder += AddItemToInventory;
        }
        else
        {
            DebugItemSystem.DebugItemAdder -= AddItemToInventory;
        }
    }

    public void AddItemToInventory(Item item)
    {
        if (!HasStateAuthority) return;

        if (ItemisGold(item)) return;

        // öncelikle bu item staklanýyor mu bak.
        if (item.stackSize > 1) // evet stacklanýyor
        {
            // bu item inventoryde var mý bak
            for (int i = 0; i < invCap; i++)
            {
                if (PLAYER_INVENTORY.ContainsKey(i))
                {
                    if (item.itemId == PLAYER_INVENTORY.Get(i).itemid)
                    {
                        // evet bu item inventoryde var stackSize 1 arttýr
                        int size = PLAYER_INVENTORY.Get(i).stackSize;
                        size++;
                        PLAYER_INVENTORY.Set(i, new(item.itemId, size));
                        PutItemToSlot(i, item, size);
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

    bool ItemisGold(Item item)
    {
        if (item.itemId == 1)
        {
            // add gold to player
            PLAYER_GOLD++;
            inventory.slot_gold_00.slotText.text = PLAYER_GOLD.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }

    void PutItemToSlot(int slotIndex, Item item, int? stackSize = null)
    {
        if (!HasStateAuthority) return;
        if (slotIndex > invCap) return;
        PLAYER_INVENTORY.Set(slotIndex, new(item.itemId, stackSize ?? 1));
        inventory.AddToInventory(slotIndex, item, stackSize ?? 1);
    }

    void RemoveItemFromInventory(int _itemId)
    {
        if (PLAYER_INVENTORY.Count <= 0) return;
        for (int i = 0; i < invCap; i++)
        {
            if (PLAYER_INVENTORY.ContainsKey(i))
            {
                if (PLAYER_INVENTORY.Get(i).itemid == _itemId)
                {
                    PLAYER_INVENTORY.Remove(i);
                    break;
                }
            }
        }
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

    public void Button_SeletedItemDelete()
    {
        if (!HasStateAuthority) return;
        RemoveItemFromInventory(inventory.GetSelectedItemId());
        inventory.SelectedItemDelete();
    }
    public void Button_SeletedItemEquip()
    {
        if (!HasStateAuthority) return;
        //RemoveItemFromInventory(inventory.GetSelectedItemId());
        //add to equipted items maybe????
        inventory.SelectedItemEquip();
    }
}
