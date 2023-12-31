using Fusion;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public struct Item : INetworkStruct
{
    public int itemId;
    public NetworkString<_32> name;
    public NetworkString<_128> description;
    public EquiptSlot slot;
    public ItemType itemType;
    public int stackSize;
    [Header("Combat Stats")]
    public byte diceCount;
    [Tooltip("Must be 4,6,8,10,12 or 20")]
    public byte damageDice;
    [Header("Defence Stats")]
    public int armor;
    public int resilience;
}

[System.Serializable]
public class ItemC
{
    public int itemId;
    public string name;
    public string description;
    public EquiptSlot slot;
    public ItemType itemType;
    public int stackSize;
    [Header("Attack Stats")]
    public byte diceCount;
    public byte damageDice;
    [Header("Defence Stats")]
    public int armor;
    public int resilicance;
}

public static class ItemEx
{
    public static Item ToItem(this ItemC itemC)
    {
        Item _item = new()
        {
            itemId = itemC.itemId,
            name = itemC.name,
            description = itemC.description,
            slot = itemC.slot,
            itemType = itemC.itemType,
            stackSize = itemC.stackSize,
            diceCount = itemC.diceCount,
            damageDice = itemC.damageDice,
            armor = itemC.armor,
            resilience = itemC.resilicance
        };
        return _item;
    }
}


public enum ItemType
{
    basic_00,
    gold_01,
    weapon_02,
    armor_03,
    quest_04,
    tools_05,
    food_06,
    potion_07,
    book_08,
    scroll_09,
    gem_10
}

public enum EquiptSlot
{
    none_00 = 0,            // none equitslotu sadece �antaya konulur, di�erleri gerekli yerlere konulur
    head_01 = 1,
    chest_02 = 2,
    hand_03 = 3,
    foot_04 = 4,
    trinket0_05 = 5,
    trinket1_06 = 6,
    weapon0_07 = 7,             // main Hand (main hand olan item off hande de konur)
    weapon1_08 = 8,             // off hand or shild
    ammo_09 = 9,
    selectedSlot = 10,
}
