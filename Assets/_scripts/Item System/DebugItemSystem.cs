using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemSystem : MonoBehaviour
{
    public static Action<Item> DebugItemAdder;
    WaitForSeconds wfs;
    bool canAddGold;
    private void Start()
    {
        wfs = new(.1f);
        canAddGold = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!canAddGold) return;
            StartCoroutine(AddGoldCoroutin());
        }
    }

    IEnumerator AddGoldCoroutin()
    {
        int maxGold = UnityEngine.Random.Range(1, 11);
        AlertText.ins.AddText("Adding " + maxGold + " gold", Color.yellow);
        canAddGold = false;
        for (int i = 0; i < maxGold; i++)
        {
            yield return wfs;
            AddGold();
            if (i >= maxGold - 1)
            {
                canAddGold = true;
                yield return null;
            }
        }
    }

    void AddGold()
    {
        // adding gold
        // 1 id item is gold 
        DebugItemAdder?.Invoke(ItemDatabase.ITEM_DATABASE[1]);
    }

    void AddRandomItem()
    {
        int i = UnityEngine.Random.Range(0, ItemDatabase.ITEM_DATABASE.Count);
        DebugItemAdder?.Invoke(ItemDatabase.ITEM_DATABASE[i]);
    }
}
