using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownClassSelect : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdownRace;
    [SerializeField] bool onValidate;

    void OnValidate()
    {
        dropdownRace = GetComponent<TMP_Dropdown>();
        ChangeToRaces();
    }

    void ChangeToRaces()
    {
        dropdownRace.ClearOptions();
        //var en = System.Enum.GetValues(typeof(Races));
        List<string> _races = new();
        foreach (var item in System.Enum.GetValues(typeof(Classes)))
        {
            _races.Add(item.ToString());
        }
        dropdownRace.AddOptions(_races);
    }
}
