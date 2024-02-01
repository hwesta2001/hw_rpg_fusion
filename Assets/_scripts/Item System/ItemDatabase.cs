/*  ITEM_DATABASE Notes:
    0 id key is must be null item
    1 id key is gold item 
    diðer tüm idler unique olmalýdýr. cvs dosyasýnda sýra ile oluþturulur.
    you get Item with id "public static Item GetItem(int itemid)"
    DATABASE BUILD YAPILMASI GEREKLÝDÝR
        Bu database Build almadan oluþturulmalý Start vb den cýkarýlmalýdýr.    
*/

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static Dictionary<int, Item> ITEM_DATABASE { get; private set; } = new();
    [SerializeField] List<Item> itemDatabase_List = new(); // ITEM_DATABASE Dictinonary inspectorden görmek için 
    string fil;
    ItemCVS[] items;
    readonly string itemsPath = "Assets/_scripts/Item System/item_cvs.csv";

    void Start()
    {
        Debug.Log("Building item dataBase. Remove before build.");
        Debug.Log("Dont forget Build Database in inspector");
        BuildItemDatabase();
    }
    [ContextMenu("BuildDateBase")]
    void BuildItemDatabase()
    {
        ITEM_DATABASE.Clear();
        itemDatabase_List.Clear();
        fil = File.ReadAllText(itemsPath);
        items = CSVSerializer.Deserialize<ItemCVS>(fil);
        foreach (ItemCVS _item in items)
        {
            itemDatabase_List.Add(_item.ToItem());
        }
        foreach (Item _item in itemDatabase_List)
        {
            AddItem(_item);
        }
        Debug.LogWarning("Item List     lenght : " + itemDatabase_List.Count);
        Debug.LogWarning("ITEM_DATABASE lenght : " + ITEM_DATABASE.Count);
        Debug.LogWarning("ID[0].id " + ITEM_DATABASE[0].itemId.ToString());
        Debug.LogWarning("ID[1].name " + ITEM_DATABASE[1].name.ToString());
    }

    public static void AddItem(Item _item)
    {
        ITEM_DATABASE.Add(_item.itemId, _item);
    }

    public static Item GetItem(int itemid)
    {
        if (ITEM_DATABASE.ContainsKey(itemid))
            return ITEM_DATABASE[itemid];
        else
            return ITEM_DATABASE[0]; // 0 id is null_item.
    }
}
