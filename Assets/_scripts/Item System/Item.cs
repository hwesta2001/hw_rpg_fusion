using Fusion;
using System.Runtime.CompilerServices;

[System.Serializable]
public struct Item : INetworkStruct
{
    public byte itemId;
    public NetworkString<_32> name;
    public NetworkString<_64> description;
    public EquiptSlot slot;
    public ItemType itemType;
}

[System.Serializable]
public class ItemC
{
    public byte itemId;
    public string name;
    public string description;
    public EquiptSlot slot;
    public ItemType itemType;
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
            itemType = itemC.itemType
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
    none_00 = 0, // none equitslotu sadece çantaya konulur, diðerleri gerekli yerlere konulur
    head_01 = 1,
    chest_02 = 2,
    hand_03 = 3,
    foot_04 = 4,
    trinket0_05 = 5,
    trinket1_06 = 6,
    weapon0_07 = 7, // main Hand (main hand olan item off hande de konur)
    weapon1_08 = 8, // off hand or shild
    ammo_09 = 9
}
