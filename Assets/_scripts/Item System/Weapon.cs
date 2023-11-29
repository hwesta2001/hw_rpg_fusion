using Fusion;
using UnityEngine;

[System.Serializable]
public struct Weapon : INetworkStruct
{
    public byte itemId;
    public NetworkString<_32> name;
    public NetworkString<_64> description;
    public EquiptSlot slot;
    public ItemType itemType;
    [Header("Combat Stats")]
    public byte diceCount;
    public Dice damageDice;

    public readonly byte MaxDamage()
    {
        return damageDice switch
        {
            Dice.D4 => 4,
            Dice.D6 => 6,
            Dice.D8 => 8,
            Dice.D10 => 10,
            Dice.D12 => 12,
            Dice.D20 => 20,
            _ => 0,
        };
    }
}


