using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct Item : INetworkStruct
{
    public byte itemId;
    public NetworkString<_32> name;
    public NetworkString<_64> description;
    public EquiptSlot slot;
    public ItemType itemType;
}

public enum ItemType
{
    basic,
    gold,
    weapon,
    armor,
    quest,
    tools,
    food,
    potion,
    book,
    scroll,
    gem
}

public enum EquiptSlot
{
    none = 0, // none equitslotu sadece çantaya konulur, diðerleri gerekli yerlere konulur
    head = 1,
    chest = 2,
    hand = 3,
    foot = 4,
    trinket0 = 5,
    trinket1 = 6,
    weapon0 = 7, // main Hand (main hand olan item off hande de konur)
    weapon1 = 8, // off hand or shild
    ammo = 9
}

public static class JsonWritter
{
    public static string SetJson(this Item item)
    {
        return JsonUtility.ToJson(item);
    }
    public static Item GetJson(string item)
    {
        return JsonUtility.FromJson<Item>(item);
    }

    public static void SaveJson(this Item item)
    {
        string savePath = Application.dataPath + "/_prefab/ItemData.json";
        File.WriteAllText(savePath, item.SetJson());
    }
}