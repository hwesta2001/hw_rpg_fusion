using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestFoundEvent : Events
{
    [SerializeField]
    public override EventType Event_Type { get; set; }
    public override string EventName { get; set; }
    public override string EventDescription { get; set; }

    public override void EnterEvent()
    {
    }

    public override void ExitEvent()
    {
    }
}
