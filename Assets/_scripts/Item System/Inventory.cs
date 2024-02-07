using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class Slot
{
    public EquiptSlot slot_;
    public int slotIndex;
    public RectTransform rectTransform;
    [Space]
    public Image image;
    public TextMeshProUGUI slotText;
    public bool isFull;
    public Item slotCurrentItem;
}

public class Inventory : MonoBehaviour
{
    [SerializeField] Sprite empty_Mini_background_img;
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

    int seletedSlotId;
    Item null_item { get; set; }
    [SerializeField] GameObject[] seletedButtons;
    private void Start()
    {
        CreateButtons();
        null_item = ItemDatabase.GetItem(0);
        slot_gold_00.slotText.text = 0.ToString();
        EmptySeletedSlot();
        for (int i = 0; i < slot.Length; i++)
        {
            RemoveFromInventory(i);
        }

        var obj = slot_selectedItem_26.rectTransform.GetComponentsInChildren<Button>();
        seletedButtons = new GameObject[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            seletedButtons[i] = obj[i].gameObject;
        }
        ChangeSelectedButtonActives(false);
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
    void ChangeSelectedButtonActives(bool active)
    {
        foreach (var item in seletedButtons)
        {
            item.SetActive(active);
        }
    }

    public void SelectedItemDelete()
    {
        if (seletedSlotId < 26) //selected full ile ayný þey
        {
            Slot _slot = GetSlot(seletedSlotId);
            _slot.isFull = false;
            _slot.slotText.text = "";
            _slot.slotCurrentItem = null_item;
            //_slot.image = EmptyImage();
            _slot.image.sprite = empty_Mini_background_img;
            EmptySeletedSlot();
        }
    }

    public void SelectedItemEquip()
    {
        if (seletedSlotId >= 26) return; //selected full ile ayný þey
        DebugText.ins.AddText("Equipt This Item: " + slot_selectedItem_26.slotCurrentItem.name);
    }

    public void ButtonClicked(int index)
    {
        Slot clickedSlot = GetSlot(index);


        if (index < 10)  // below 10 is equiptment sloats
        {
            // look for which slot is cliked and determine is seleted object fits this eq slot
            // if it fits put selected item to this slot and put eq_slot_item to seleted item inv_slot
        }
        else // inventory sloats begin with 10
        {
            if (clickedSlot.isFull)
            {
                SetSelectedSlot(index);
            }
            else
            {
                EmptySeletedSlot();
            }
        }

    }

    void SetSelectedSlot(int index)
    {
        seletedSlotId = index;
        slot_selectedItem_26.isFull = true;
        slot_selectedItem_26.slotCurrentItem = GetSlot(index).slotCurrentItem;
        slot_selectedItem_26.slotText.text = SetTextWithDesc(GetSlot(index).slotCurrentItem, 1);
        //slot_selectedItem_26.image = slot[index - 10].slotCurrentItemImage();
        slot_selectedItem_26.image.sprite = slot[index - 10].slotCurrentItem.GetItemIcon();
        ChangeSelectedButtonActives(true);
    }
    void EmptySeletedSlot()
    {
        seletedSlotId = 100; // a big number 
        slot_selectedItem_26.isFull = false;
        slot_selectedItem_26.slotCurrentItem = null_item;
        slot_selectedItem_26.slotText.text = "";
        //slot_selectedItem_26.image = EmptyImage();
        slot_selectedItem_26.image.sprite = empty_Mini_background_img;
        ChangeSelectedButtonActives(false);
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

    public int GetSelectedItemId()
    {
        if (!slot_selectedItem_26.isFull) return 0; //null item returning
        else return slot_selectedItem_26.slotCurrentItem.itemId;
    }

    public void AddToInventory(int slotindex, Item item, int? stackSize = null)
    {
        slot[slotindex].isFull = true;
        slot[slotindex].slotCurrentItem = item;
        //slot[slotindex].image = item.GetIcon();  // getIcon ile item iconu çek biryerlerden
        slot[slotindex].image.sprite = item.GetItemIcon();
        slot[slotindex].slotText.text = SetSlotText(item, stackSize ?? 1);
    }

    public void RemoveFromInventory(int slotindex)
    {
        slot[slotindex].isFull = false;
        slot[slotindex].slotCurrentItem = null_item;
        slot[slotindex].slotText.text = "";
        //slot[slotindex].image = item.GetIcon();  // getIcon ile item iconu çek biryerlerden
        slot[slotindex].image.sprite = empty_Mini_background_img;
    }

    string SetSlotText(Item item, int stackSize)
    {
        return SetName(item, stackSize) + "\n" + "<size=12>" + SetStats(item) + "</size>";
    }

    string SetTextWithDesc(Item item, int stackSize)
    {
        return SetName(item, stackSize) + "\n" + SetStats(item) + "\n" +
               $"<size=12><i>{item.description}</i></size>";
    }

    string SetName(Item item, int stackSize)
    {
        if (stackSize > 1) return $"<b>{item.name}</b> ({stackSize}) _{item.itemId}";  // _{item.itemId}  Debug içindir
        else return $"<b> {item.name} _{item.itemId}</b>";  // _{item.itemId}  Debug içindir
    }

    string SetStats(Item item)
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
