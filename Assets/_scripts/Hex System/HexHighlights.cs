using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexHighlights : MonoBehaviour
{
    public List<GameObject> hexHighlightObjects = new();

    [SerializeField] bool onValidateListPop;
    [SerializeField] GameObject hexHighlightPrefab;
    [SerializeField] Vector3 initSize;
    Tween[] tween = new Tween[7];
    private void OnValidate()
    {
        if (!onValidateListPop) return;
        if (hexHighlightObjects.Count > 0)
        {
            foreach (GameObject go in hexHighlightObjects)
            {
                DestroyImmediate(go);
            }
        }
        hexHighlightObjects.Clear();
        for (int i = 0; i < 7; i++)
        {
            hexHighlightObjects.Add(Instantiate(hexHighlightPrefab));
            hexHighlightObjects[i].name = "hexHiglight_0" + i;
            hexHighlightObjects[i].transform.parent = transform;
        }
    }
    public static HexHighlights ins; // singleton:..............
    private void Awake()
    {
        ins = this;
        initSize = hexHighlightObjects[6].transform.localScale;
    }

    public void MoveHexHighlight(int index, Vector3 pos)
    {
        hexHighlightObjects[index].transform.position = pos;
        hexHighlightObjects[index].SetActive(true);
        //hexHighlightObjects[index].transform.DOComplete();
        //tween.Complete();
        tween[index] = hexHighlightObjects[index].transform.DOScale(initSize * 1.11f, 1.4f).SetLoops(-1, LoopType.Yoyo);
    }

    public void DisableHexHighlights()
    {

        for (int i = 0; i < hexHighlightObjects.Count; i++)
        {
            tween[i].Kill();
            hexHighlightObjects[i].transform.localScale = initSize;
            hexHighlightObjects[i].SetActive(false);
            //item.transform.position = Vector3.up * 100;
        }
    }
}
