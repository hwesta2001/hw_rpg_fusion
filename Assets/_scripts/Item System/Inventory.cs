using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot
{
    public EquiptSlot slot_;
    public int slotIndex;
    public RectTransform rectTransform;
    public Image image;
    public TextMeshProUGUI slotText;
    [Space]
    public bool isFull;
    public Item slotCurrentItem;
}

public class Inventory : MonoBehaviour
{
    #region Slots_setInspector
    public Slot slot_selectedItem_26;
    public Slot slot_gold_00;
    public Slot slot_head_01;
    public Slot slot_chest_02;
    public Slot slot_hand_03;
    public Slot slot_foot_04;
    public Slot slot_trinket0_05;
    public Slot slot_trinket1_06;
    public Slot slot_weapon0_07;
    public Slot slot_weapon1_08;
    public Slot slot_ammo_09;
    public Slot[] slot = new Slot[16];
    #endregion

    #region Sprites
    [SerializeField] Sprite empty_gold_s;
    [SerializeField] Sprite empty_head_s;
    [SerializeField] Sprite empty_chest_s;
    [SerializeField] Sprite empty_hand_s;
    [SerializeField] Sprite empty_foot_s;
    [SerializeField] Sprite empty_trinket_s;
    [SerializeField] Sprite empty_weapon_s;
    [SerializeField] Sprite empty_ammo_s;
    [SerializeField] Sprite empty_slot_s;
    #endregion

    Item null_item;
    private void Start()
    {
        CreateButtons();
        null_item = ItemDatabase.GetItem(0);
    }

    [ContextMenu("Create Buttons")]
    void CreateButtons()
    {
        AddButton(slot_head_01);
        AddButton(slot_chest_02);
        AddButton(slot_hand_03);
        AddButton(slot_foot_04);
        AddButton(slot_trinket0_05);
        AddButton(slot_trinket1_06);
        AddButton(slot_weapon0_07);
        AddButton(slot_weapon1_08);
        AddButton(slot_ammo_09);
        foreach (var item in slot)
        {
            AddButton(item);
        }
    }

    void AddButton(Slot _slot)
    {
        Button but = _slot.rectTransform.gameObject.AddComponent<Button>();
        //but.image = _slot.image;
        but.onClick.AddListener(() => ButtonClicked(_slot.slotIndex));
    }

    public void ButtonClicked(int index)
    {
        if (index < 10)  // below 10 is equiptment sloats
        {
            Debug.Log($" equiptment slot has been clicked {DebugText(index)}");
            // look for which slot is cliked and determine is seleted object fits this eq slot
            // if it fits put selected item to this slot and put eq_slot_item to seleted item inv_slot
        }
        else // inventory sloats begin with 10
        {
            Debug.Log($"Inventory slot has been clicked {DebugText(index)}");
            // 
        }

    }

    string DebugText(int index)
    {
        string textt = "";
        if (index == 1) textt += "head";
        if (index == 2) textt += "chest";
        if (index == 3) textt += "hand";
        if (index == 4) textt += "foot";
        if (index == 5) textt += "trinket0";
        if (index == 6) textt += "trinket1";
        if (index == 7) textt += "weapon0";
        if (index == 8) textt += "weapon1";
        if (index == 9) textt += "ammo";
        if (index > 9) textt += "inv_slot_" + (index - 10);
        return textt;
    }

    public void AddToInventory(int slotindex, Item item, int? stackSize = null)
    {
        slot[slotindex].isFull = true;
        slot[slotindex].slotCurrentItem = item;
        //slot[slotindex].image = item.GetIcon();  // getIcon ile item iconu çek biryerlerden
        slot[slotindex].slotText.text = SetText(item, stackSize ?? 1);
    }

    public void RemoveFromInventory(int slotindex)
    {
        slot[slotindex].isFull = false;
        slot[slotindex].slotCurrentItem = null_item;
        //slot[slotindex].image = item.GetIcon();  // getIcon ile item iconu çek biryerlerden
        slot[slotindex].slotText.text = "";
    }

    string SetText(Item item, int stackSize)
    {
        string txt = "";
        if (stackSize > 1) txt += $"<b>{item.name}>/b> ({stackSize})";
        else txt += $"<b>{item.name}>/b>";
        //txt += "/n"+Stats(item);
        return txt;
    }
    string SetTextWithDesc(Item item, int stackSize)
    {
        string txt = "";
        txt += $"<b>{item.name}>/b>/n";
        txt += Stats(item);
        txt += $"<i>{item.description}</i>";
        return txt;
    }

    string Stats(Item item)
    {
        string txt = "";
        if (item.diceCount != 0)
        {
            txt += $"<color=red>Damage: {item.diceCount} x 1d{item.damageDice} </color>";
        }
        if (item.armor != 0 && item.resilience != 0)
        {
            txt += $"<color=magenta>Armor: {item.armor} Resilience {item.resilience} </color>";
        }
        if (item.armor != 0 && item.resilience == 0)
        {
            txt += $"<color=magenta>Armor: {item.armor} </color>";
        }
        if (item.armor == 0 && item.resilience != 0)
        {
            txt += $"<color=blue>Resilience: {item.resilience} </color>";
        }
        txt += "/n";
        return txt;
    }

    public bool freeSlotAvaliable(out int slotindex)
    {
        bool fsa = false;
        int index = 100;
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].isFull == false)
            {
                fsa = true;
                index = i;
                break;
            }
        }
        slotindex = index;
        return fsa;
    }
}
