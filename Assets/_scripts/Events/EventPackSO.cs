using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Pack", menuName = "Events", order = 1)]
public class EventPackSO : ScriptableObject
{
    [SerializeField] string Description = "Events pack of ...";
    [Range(0, 100)] public int priority = 10;
    public List<Events> events;

}
