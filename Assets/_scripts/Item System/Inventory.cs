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

    Item null_item { get; set; }
    private void Start()
    {
        CreateButtons();
        null_item = ItemDatabase.GetItem(0);
        slot_gold_00.slotText.text = 0.ToString();
        EmptySeletedSlot();
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
        Slot clickedSlot = GetSlot(index);
        if (clickedSlot.isFull)
        {
            slot_selectedItem_26.slotText.text = SetTextWithDesc(slot[index - 10].slotCurrentItem, 1);
        }
        else
        {
            EmptySeletedSlot();
        }

        if (index < 10)  // below 10 is equiptment sloats
        {
            // look for which slot is cliked and determine is seleted object fits this eq slot
            // if it fits put selected item to this slot and put eq_slot_item to seleted item inv_slot
        }
        else // inventory sloats begin with 10
        {

        }

    }

    Slot GetSlot(int index)
    {
        if (index == 0) return slot_gold_00;
        if (index == 1) return slot_head_01;
        if (index == 2) return slot_chest_02;
        if (index == 3) return slot_hand_03;
        if (index == 4) return slot_foot_04;
        if (index == 5) return slot_trinket0_05;
        if (index == 6) return slot_trinket1_06;
        if (index == 7) return slot_weapon0_07;
        if (index == 8) return slot_weapon1_08;
        if (index == 9) return slot_ammo_09;
        if (index > 9) return slot[index - 10];
        else return null;
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
        slot[slotindex].slotText.text = "";
        //slot[slotindex].image = item.GetIcon();  // getIcon ile item iconu çek biryerlerden
    }

    void EmptySeletedSlot()
    {
        slot_selectedItem_26.isFull = false;
        slot_selectedItem_26.slotCurrentItem = null_item;
        slot_selectedItem_26.slotText.text = "";
        //slot_selectedItem_26.image = EmptyImage();
    }

    string SetText(Item item, int stackSize)
    {
        string txt = "";
        if (stackSize > 1) txt += $"<b>{item.name}</b> ({stackSize})";
        else txt += $"<b>{item.name}</b>";
        //txt += "\n"+Stats(item);
        return txt;
    }
    string SetTextWithDesc(Item item, int stackSize)
    {
        string txt = "";
        txt += $"<b>{item.name}</b>\n";
        txt += Stats(item) + "\n";
        txt += $"<size=18><i>{item.description}</i></size>";
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
            txt += $"<color=purple>Armor: {item.armor}</color><color=blue> Resilience {item.resilience} </color>";
        }
        if (item.armor != 0 && item.resilience == 0)
        {
            txt += $"<color=purple>Armor: {item.armor} </color>";
        }
        if (item.armor == 0 && item.resilience != 0)
        {
            txt += $"<color=blue>Resilience: {item.resilience} </color>";
        }
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
