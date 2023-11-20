using UnityEngine;

public abstract class IEvents : MonoBehaviour
{
    public abstract EventType Event_Type { get; set; }
    public abstract string EventName { get; set; }
    public abstract string EventDescription { get; set; }
    public abstract void EnterEvent();
    public abstract void ExitEvent();
}

public enum EventType
{
    GoodEvent,
    BadEvent,
    QuestEvent,
    TownEvent,
    CombatWin,
    CombatLost
}
