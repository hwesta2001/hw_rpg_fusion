using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSheetCreate : MonoBehaviour
{
    public GameObject charSheetPrefab;
    [SerializeField] List<CharSheetSet> poolSheets = new();
    Canvas thisCanvas;

    private void Awake()
    {
        thisCanvas = GetComponent<Canvas>();
    }

    public void SetGo()
    {
        if (RayCastEvent.ins.HittedObject == null) return;
        if (RayCastEvent.ins.HittedObject.transform.root.TryGetComponent(out PLAYER _player))
        {
            if (ControlCharSheet(_player.CHAR_NW.playerID)) return;
            AddSheetsToList(_player.CHAR_NW);
        }
    }

    void AddSheetsToList(CharNW charNW)
    {
        //if (!HasStateAuthority) return;
        GameObject _go = Instantiate(charSheetPrefab, parent: thisCanvas.transform);
        _go.SetActive(false);
        CharSheetSet charSheet = _go.GetComponent<CharSheetSet>();
        charSheet.SetSheet(charNW);
        poolSheets.Add(charSheet);

        RectTransform rectTransform = _go.GetComponent<RectTransform>();
        //rectTransform.SetParent(thisCanvas.transform, false);
        rectTransform.localScale = Vector3.one;
        rectTransform.anchoredPosition = new(20f + charNW.playerID * 3f, -60f);
        _go.GetComponentInChildren<Dragable>().canvas = thisCanvas;
        _go.SetActive(true);
    }

    bool ControlCharSheet(byte id)
    {
        foreach (CharSheetSet item in poolSheets)
        {
            if (item._playerId == id)
            {
                item.Relocate();
                item.gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
