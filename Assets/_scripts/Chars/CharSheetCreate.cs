using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSheetCreate : MonoBehaviour
{
    public GameObject go;
    public CharNW charNW;
    [SerializeField] GameObject charSheetPrefab;
    [SerializeField] List<CharSheetSet> poolSheets = new();
    public void SetGo()
    {

        if (RayCastEvent.ins.HittedObject != null)
        {

            go = RayCastEvent.ins.HittedObject;
            charNW = go.GetComponentInParent<PLAYER>().CHAR_NW;
            GetSheetsFromPool(charNW.playerID).SetSheet(charNW);
        }
    }


    void AddSheetsToList(GameObject _go, Transform _parent)
    {
        _go.SetActive(true);
        _go.GetComponent<RectTransform>().SetParent(_parent, false);
        _go.GetComponent<RectTransform>().localScale = Vector3.one;
        _go.GetComponent<RectTransform>().anchoredPosition = new(20f, -65f);
        _go.GetComponentInChildren<Dragable>().canvas = GetComponent<Canvas>();
        poolSheets.Add(_go.GetComponent<CharSheetSet>());
    }

    CharSheetSet GetSheetsFromPool(byte id)
    {
        for (int i = 0; i < poolSheets.Count; i++)
        {

            if (poolSheets[i]._playerId == id)
            {
                poolSheets[i].gameObject.SetActive(true);
                return poolSheets[i];
            }

            if (!poolSheets[i].gameObject.activeInHierarchy)
            {
                return poolSheets[i];
            }
        }

        AddToSheeetPool(1);
        return GetSheetsFromPool(id);
    }

    void AddToSheeetPool(int howMuch)
    {

        for (int i = 0; i < howMuch; i++)
        {
            AddSheetsToList(Instantiate(charSheetPrefab), transform);
        }
    }
}
