using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class Extentions
{
    public static void ToLog(this string text)
    {
        DebugLogs.Ins.ToLog(text);
    }

    public static CharNW GetChar(this int id)
    {
        return CharManager.ins.GetChar(id);
    }

    public static async Task WaitUntil(Func<bool> predicate, int sleep = 50)
    {
        while (!predicate())
        {
            await Task.Delay(sleep);
        }
    }

    //    list: List<T> to resize
    //    size: desired new size
    // element: default value to insert
    public static void Resize<T>(this List<T> list, int size, T element = default(T))
    {
        int count = list.Count;

        if (size < count)
        {
            list.RemoveRange(size, count - size);
        }
        else if (size > count)
        {
            if (size > list.Capacity)   // Optimization
                list.Capacity = size;

            list.AddRange(Enumerable.Repeat(element, size - count));
        }
    }

    public static void DebugColor(this string message, string hex_color_code = null)
    {
        Debug.Log($"<color={hex_color_code ?? "#F9EEE4"}>{message}</color>");
    }

    public static Sprite GetItemIcon(this Item item)   // !!! Ana Icon cekme metodu !!!
    {
        return ItemIconDatabase.Ins.GetItemIcon(item);
    }
}
