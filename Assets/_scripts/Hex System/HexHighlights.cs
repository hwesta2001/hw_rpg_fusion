using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexHighlights : MonoBehaviour
{
    public static HexHighlights Ins { get; set; } // singleton:..............

    public List<GameObject> hexHighlightObjects = new();

    [SerializeField] bool onValidateListPop;
    [SerializeField] Vector3 initSize;
    Tween[] tween = new Tween[7];

    private void OnValidate()
    {
        if (!onValidateListPop) return;
        Init();

    }
    private void Awake() => Ins = this;

    private void Start()
    {
        Init();
        DisableHexHighlights();
    }
    private void Init()
    {
        initSize = hexHighlightObjects[0].transform.localScale;
        for (int i = 1; i < hexHighlightObjects.Count; i++)
        {
            GameObject go = hexHighlightObjects[i];
            Destroy(go);
        }
        hexHighlightObjects.Resize(1);
        for (int i = 1; i < 7; i++)
        {
            hexHighlightObjects.Insert(i, Instantiate(hexHighlightObjects[0]));
            hexHighlightObjects[i].name = "hexHiglight_0" + i;
            hexHighlightObjects[i].transform.parent = transform;
            hexHighlightObjects[i].transform.localScale = initSize;
        }
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
