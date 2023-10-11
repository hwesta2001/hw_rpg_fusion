using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexToMap : MonoBehaviour

{
    [SerializeField] bool onValidate;
    [SerializeField] HexGridGenerator gridGenerator;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] Vector2 hexesSize;

    void OnValidate()
    {
        if (gridGenerator == null) gridGenerator = FindAnyObjectByType<HexGridGenerator>();
        if (mapGenerator == null) mapGenerator = FindAnyObjectByType<MapGenerator>();
        gridGenerator.OnGridCreate += ChangeSize;
        gridGenerator.OnGridCreate += GenerateMap;
        ChangeSize();
    }
    void OnDisable()
    {
        gridGenerator.OnGridCreate -= GenerateMap;
        gridGenerator.OnGridCreate -= ChangeSize;
    }

    public void ChangeSize()
    {
        hexesSize = gridGenerator.hexesSize();
        transform.localScale = new(hexesSize.x * .1f, 1, hexesSize.y * .1f);
    }

    public void GenerateMap()
    {
        mapGenerator.GererateMapEmit(gridGenerator.mapWidht, gridGenerator.mapHeight);
        ScaleHexes();
    }


    void ScaleHexes()
    {
        foreach (var item in gridGenerator.hexes)
        {
            item.transform.localScale = Vector3.one;
        }
        for (int i = 0; i < gridGenerator.hexes.Count; i++)
        {
            gridGenerator.hexes[i].transform.localScale = new(1, 1 + 200 * mapGenerator.allNoises[i].height, 1);

        }
    }

}
