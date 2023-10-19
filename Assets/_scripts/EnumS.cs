using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumS
{

}

public enum Dice { D4, D6, D8, D10, D12, D20 }

public enum Races
{
    Human,
    Elf,
    Orc,
    Dwarf,
    Halfling,
    Gnome
}

public enum Classes
{
    Fighter,
    Wizard,
    Rogue,
    Paladin,
    Druid,
    Warlock,
    Bard,
    Barbarian,
    Cleric,
    Monk,
}

public enum GameStates
{
    Init,
    Connected,
    InGameLoop,
    Waiting,
    Lost
}
