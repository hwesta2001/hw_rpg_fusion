using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharGetAndSet : MonoBehaviour
{
    [SerializeField] Chars gettedChar;

    string _name;
    Races _race;
    Classes _classes;
    string _desc;

    //gererate and deside someway.
    //byte strength = 8;
    //byte agility = 8;
    //byte intelligence = 8;
    //byte charisma = 8;
    //byte luck = 8;

    int _portrait; //gererate and deside someway.

    [SerializeField] TMP_InputField name_inputField;
    [SerializeField] TMP_Dropdown raceDropdown;
    [SerializeField] TMP_Dropdown classDropdown;
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] TMP_InputField desc_inputField;
    [SerializeField] Image portrait;

    private void Start()
    {
        gettedChar = new();
        DiceRandomStats();
    }

    [ContextMenu("SetChar")]
    public void SetChar()
    {
        gettedChar.name = name_inputField.text;
        gettedChar.race = (Races)raceDropdown.value;
        gettedChar.classes = (Classes)classDropdown.value;
        gettedChar.desc = desc_inputField.text;
    }

    public void SetPortraitId(int _id)
    {
        gettedChar.portraitId = (byte)_id;
    }

    public void SetPortraitOnCharSheet(Sprite sprite)
    {
        portrait.sprite = sprite;
    }
    public Chars GetChar()
    {
        return gettedChar;
    }


    public void DiceRandomStats() // add a buton;
    {
        SetStats((byte)(Random.Range(0, 11) + 8),
                 (byte)(Random.Range(0, 11) + 8),
                 (byte)(Random.Range(0, 11) + 8),
                 (byte)(Random.Range(0, 11) + 8),
                 (byte)(Random.Range(0, 11) + 8));
    }





    void SetStats(byte str, byte agi, byte intel, byte chrsm, byte luck)
    {
        gettedChar.strength = str;
        gettedChar.agility = agi;
        gettedChar.intelligence = intel;
        gettedChar.charisma = chrsm;
        gettedChar.luck = luck;
        SetStatsOnCharSheet();
    }

    public void SetStatsOnCharSheet()
    {
        int tot = gettedChar.strength + gettedChar.agility + gettedChar.intelligence + gettedChar.charisma + gettedChar.luck;
        ;

        statText.text = "Strenght: " + gettedChar.strength.ToString() + "\n" +
                        "Agility: " + gettedChar.agility.ToString() + "\n" +
                        "Intelligent: " + gettedChar.intelligence.ToString() + "\n" +
                        "Charisma: " + gettedChar.charisma.ToString() + "\n" +
                        "Luck: " + gettedChar.luck.ToString() + "\n" +
                        "<b>TOTAL: " + tot.ToString() + "</b>";
    }
}
