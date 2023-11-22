using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharGetAndSet : MonoBehaviour
{
    [SerializeField] CharBeforeNetwork gettedChar;
    [SerializeField] TMP_InputField name_inputField;
    [SerializeField] TMP_Dropdown raceDropdown;
    [SerializeField] TMP_Dropdown classDropdown;
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] TMP_InputField desc_inputField;
    [SerializeField] RawImage portrait;

    private void Start()
    {
        gettedChar = new();
        DiceRandomStats();
        SetPortraitId(2);
        name_inputField.text = GetRandomName();
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
        portrait.texture = CharManager.ins.portList[_id];
    }
    public CharBeforeNetwork GetChar()
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


    #region SaveLoad RolledStats
    byte[] savedStats = { 8, 8, 8, 8, 8 };
    public void SaveStats() // to button
    {
        savedStats[0] = gettedChar.strength;
        savedStats[1] = gettedChar.agility;
        savedStats[2] = gettedChar.intelligence;
        savedStats[3] = gettedChar.charisma;
        savedStats[4] = gettedChar.luck;

    }

    public void LoadSavedStats() // to button
    {
        SetStats(savedStats[0], savedStats[1], savedStats[2], savedStats[3], savedStats[4]);
    }
    #endregion


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

    string GetRandomName()
    {
        string[] genres = new string[] { "Thaddeus Lujan", "Kristofer", "Jamal Mireles", "Aliah Frey", "Payne Prince", "Axel", "Ayleen", "Miguelangel Summers", "Christianna High", "Miya Pelletier", "Finn", "Kerry Shumate", "Marcus", "Travon", "Mason", "Tyrique", "Baylor Thurman", "Taya Healey", "Fidel Bingham", "Hans", "Yessica", "Simran Kolb", "Sariah Millard", "Rowan Barlow", "Kadin", "Jazmyn", "Mia", "Sam Kaplan", "Arman", "Edgardo Cope", "Enrique Rawlings", "Bobbie", "Adrian Manning", "Norman", "Quinlan Coles", "Brylee Salisbury", "Avery Peek", "Eboni", "Annmarie", "Lars Richmond", "Carlton Luciano", "Jan Merritt", "Brycen", "Brendon Andrade", "Mason Koenig", "Myia Aguayo", "Jaylynn", "Coleton", "Sarah Mahoney", "Daysha" };
        return genres[Random.Range(0, genres.Length)];
    }
}
