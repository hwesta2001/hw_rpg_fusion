using UnityEngine;

public class RandomHealEvent : IEvents
{
    [field: SerializeField] public override EventType Event_Type { get; set; }
    [field: SerializeField] public override string EventName { get; set; }
    [field: SerializeField, TextArea(2, 10)] public override string EventDescription { get; set; }

    public override void EnterEvent()
    {
    }

    public override void ExitEvent()
    {
    }
}
