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
    Init = 0,
    Connected = 1,
    InGameLoop = 2,
    Waiting = 3,
    Lost = 4
}
