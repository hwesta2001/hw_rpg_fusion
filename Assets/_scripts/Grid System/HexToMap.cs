using System.Reflection;
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
        ColorHexes();
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


    void ColorHexes()
    {



        for (int i = 0; i < gridGenerator.hexes.Count; i++)
        {
            MeshRenderer mRend = gridGenerator.hexes[i].transform.GetComponentInChildren<MeshRenderer>();
            Material mat = mRend.material;
            mat.color = mapGenerator.allNoises[i].colour;
            mRend.material = mat;

        }
        //    ClearLog();
        //}

        //void ClearLog()
        //{
        //    var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        //    var type = assembly.GetType("UnityEditor.LogEntries");
        //    var method = type.GetMethod("Clear");
        //    method.Invoke(new object(), null);
    }
}
