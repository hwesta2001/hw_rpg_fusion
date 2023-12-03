using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public ItemC[] items;
    public List<Item> ITEM_DATABASE { get; private set; } = new();
    readonly string itemsPath = "Assets/_scripts/Item System/item_cvs.csv";
    [SerializeField] string fil;

    [ContextMenu("BuildDateBase")]
    void BuildItemDatabase()
    {

        ITEM_DATABASE.Clear();
        fil = File.ReadAllText(itemsPath);
        items = CSVSerializer.Deserialize<ItemC>(fil);
        foreach (ItemC item in items)
        {
            ITEM_DATABASE.Add(item.ToItem());
        }
    }
}
