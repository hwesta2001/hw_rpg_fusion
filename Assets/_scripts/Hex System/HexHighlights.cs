using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexHighlights : MonoBehaviour
{
    public List<GameObject> hexHighlightObjects = new();

    [SerializeField] bool onValidateListPop;
    [SerializeField] GameObject hexHighlightPrefab;
    Vector3 initSize;
    private void OnValidate()
    {
        if (!onValidateListPop) return;
        hexHighlightObjects.Clear();
        for (int i = 0; i < 7; i++)
        {
            hexHighlightObjects.Add(Instantiate(hexHighlightPrefab));
            hexHighlightObjects[i].name = "hexHiglight_0" + i;
        }
    }
    public static HexHighlights ins; // singleton:..............
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        initSize = hexHighlightObjects[0].transform.localScale;
    }

    public void MoveHexHighlight(int index, Vector3 pos)
    {
        hexHighlightObjects[index].transform.position = pos;
        hexHighlightObjects[index].SetActive(true);
        hexHighlightObjects[index].transform.DOComplete();
        hexHighlightObjects[index].transform.DOScale(initSize * 1.3f, 2).SetLoops(-1, LoopType.Yoyo);
    }

    public void DisableHexHighlights()
    {
        foreach (var item in hexHighlightObjects)
        {
            item.SetActive(false);
            item.transform.position = Vector3.up * 100;
        }
    }
}
