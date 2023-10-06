using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownRaceSelect : MonoBehaviour
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
        foreach (var item in System.Enum.GetValues(typeof(Races)))
        {
            _races.Add(item.ToString());
        }
        dropdownRace.AddOptions(_races);
    }

}
