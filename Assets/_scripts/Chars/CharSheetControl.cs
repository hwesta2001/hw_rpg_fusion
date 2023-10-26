using System.Collections.Generic;
using UnityEngine;

public class CharSheetControl : MonoBehaviour
{
    public GameObject charSheetPrefab;
    [SerializeField] List<CharSheetSet> poolSheets = new();
    Canvas thisCanvas;
    public static CharSheetControl ins;
    private void Awake()
    {
        ins = this;
        thisCanvas = GetComponent<Canvas>();
    }

    public void SetGo()
    {
        if (RayCastEvent.ins.HittedObject == null) return;
        if (RayCastEvent.ins.HittedObject.transform.root.TryGetComponent(out PLAYER _player))
        {
            //if (ControlCharSheet(_player.CHAR_NW.playerID)) return;
            //AddSheetsToList(_player.CHAR_NW);
            OpenCharSheet(_player.CHAR_NW);
        }
    }


    public void OpenCharSheet(CharNW cn)
    {
        if (ControlCharSheet(cn.playerID)) return;
        AddSheetsToList(cn);
    }

    void AddSheetsToList(CharNW charNW)
    {
        //if (!HasStateAuthority) return;
        GameObject _go = Instantiate(charSheetPrefab, parent: thisCanvas.transform);
        _go.SetActive(false);
        CharSheetSet charSheet = _go.GetComponent<CharSheetSet>();
        charSheet.SetSheet(charNW);
        poolSheets.Add(charSheet);

        //RectTransform rectTransform = _go.GetComponent<RectTransform>();
        ////rectTransform.SetParent(thisCanvas.transform, false);
        //rectTransform.localScale = Vector3.one;
        //charSheet.Relocate(charNW.playerID);
        _go.GetComponentInChildren<Dragable>().canvas = thisCanvas;
        _go.SetActive(true);
    }

    bool ControlCharSheet(byte id)
    {
        foreach (CharSheetSet item in poolSheets)
        {
            if (item._playerId == id)
            {
                item.Relocate(id);
                item.gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
