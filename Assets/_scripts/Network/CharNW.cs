using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct CharNW : INetworkStruct
{
    public byte playerID;
    public NetworkString<_16> name;
    public Races race;
    public Classes classes;
    public NetworkString<_512> desc;
    public byte portraitId;
    [Space]
    public byte level;
    [Space]
    public byte strength;
    public byte agility;
    public byte intelligence;
    public byte charisma;
    public byte luck;
    [Space]
    public NetworkBool isTurnReady;
    [Space]
    //Health Sistem ayarlanacak
    public int MaxHealth;
    public int CurrentHealth;
}