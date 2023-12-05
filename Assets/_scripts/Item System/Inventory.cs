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

    private void Start()
    {
        CreateButtons();
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
        DebugText(index);
    }

    void DebugText(int index)
    {
        string textt = "slot selected: ";
        if (index == 1) textt += "head";
        if (index == 2) textt += "chest";
        if (index == 3) textt += "hand";
        if (index == 4) textt += "foot";
        if (index == 5) textt += "trinket0";
        if (index == 6) textt += "trinket1";
        if (index == 7) textt += "weapon0";
        if (index == 8) textt += "weapon1";
        if (index == 9) textt += "ammo";
        if (index > 9) textt += "bag_slot_" + (index - 10);
        AlertText.ins.AddText(textt, Color.grey);
    }


}
