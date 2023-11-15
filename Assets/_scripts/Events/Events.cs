using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Events : MonoBehaviour
{
    [property: SerializeField] public abstract EventType Event_Type { get; set; }
    [property: SerializeField] public abstract string EventName { get; set; }
    [property: SerializeField] public abstract string EventDescription { get; set; }
    public abstract void EnterEvent();
    public abstract void ExitEvent();
}
