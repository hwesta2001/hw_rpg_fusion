using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chars
{
    public string name;
    public Races race;
    public Classes classes;
    public string desc;
    public byte portraitId;
    [Space]
    public byte strength = 8;
    public byte agility = 8;
    public byte intelligence = 8;
    public byte charisma = 8;
    public byte luck = 8;
}