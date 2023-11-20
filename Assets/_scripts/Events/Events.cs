using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Events : MonoBehaviour
{
    [SerializeField] bool onValiteTrigger = true;
    [SerializeField] string description;
    [Range(0, 100)] public int priority = 50;
    public List<IEvents> i_events = new();

    private void OnValidate()
    {
        if (!onValiteTrigger) { return; }
        SetList();
    }
    void SetList()
    {
        i_events.Clear();
        foreach (var e in GetComponentsInChildren<IEvents>())
        {
            i_events.Add(e);
        }
    }
}