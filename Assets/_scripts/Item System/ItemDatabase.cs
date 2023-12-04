using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static Dictionary<int, Item> ITEM_DATABASE { get; private set; } = new();
    [SerializeField] List<Item> itemDatabase_List = new(); // ITEM_DATABASE Dictinonary inspectorden görmek için 
    [SerializeField] string fil;
    ItemC[] items;
    readonly string itemsPath = "Assets/_scripts/Item System/item_cvs.csv";

    [ContextMenu("BuildDateBase")]
    void BuildItemDatabase()
    {
        ITEM_DATABASE.Clear();
        itemDatabase_List.Clear();
        fil = File.ReadAllText(itemsPath);
        items = CSVSerializer.Deserialize<ItemC>(fil);
        foreach (ItemC _item in items)
        {
            itemDatabase_List.Add(_item.ToItem());
        }
        foreach (Item _item in itemDatabase_List)
        {
            AddItem(_item);
        }
        Debug.Log("Item List     lenght : " + itemDatabase_List.Count);
        Debug.Log("ITEM_DATABASE lenght : " + ITEM_DATABASE.Count);
    }

    public static void AddItem(Item _item)
    {
        ITEM_DATABASE.Add(_item.itemId, _item);
    }
}
