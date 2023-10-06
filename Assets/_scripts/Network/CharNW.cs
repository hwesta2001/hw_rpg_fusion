using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//public class CharNWControler
//{
[System.Serializable]
public struct CharNW : INetworkStruct
{
    public byte playerID;
    public NetworkString<_16> name;
    public Races race;
    public Classes classes;
    public NetworkString<_512> desc;
    public byte portraitId;
    public byte strength;
    public byte agility;
    public byte intelligence;
    public byte charisma;
    public byte luck;
}
//}
