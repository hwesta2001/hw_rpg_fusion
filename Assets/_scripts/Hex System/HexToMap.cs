using UnityEngine;

public class HexToMap : MonoBehaviour

{
    [SerializeField] bool canChangeHexMaterial;
    [SerializeField] HexGrid gridGenerator;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] Vector2 hexesSize;
    [SerializeField] Transform mapPlane;

    [ContextMenu("OnGridCreateReset")]
    void OnGridCreateReset()
    {
        //gridGenerator.OnGridCreate = GenerateMap;
        gridGenerator.OnGridCreate = null;
        gridGenerator.OnGridCreate += GenerateMap;
    }
    private void OnValidate()
    {
        gridGenerator.OnGridCreate = GenerateMap;
    }
    void OnDisable()
    {
        gridGenerator.OnGridCreate -= GenerateMap;
    }

    public void GenerateMap()
    {
        ChangeSize();
        mapGenerator.GererateMapEmit(gridGenerator.mapWidht, gridGenerator.mapHeight);
        if (!canChangeHexMaterial) return;
        //ScaleHexes();
        ColorHexes();
    }

    public void ChangeSize()
    {
        hexesSize = gridGenerator.HexesSize();
        mapPlane.localScale = new(hexesSize.x * .1f, 1, hexesSize.y * .1f);
    }


    void ScaleHexes()
    {
        foreach (var item in gridGenerator.hexes_Objects)
        {
            item.transform.localScale = Vector3.one;
        }
        for (int i = 0; i < gridGenerator.hexes_Objects.Count; i++)
        {
            gridGenerator.hexes_Objects[i].transform.localScale = new(1, 1 + 200 * mapGenerator.allMapRegions[i].height, 1);

        }
    }


    void ColorHexes()
    {
        for (int i = 0; i < gridGenerator.hexes_Renderer.Count; i++)
        {
            gridGenerator.hexes_Renderer[i].sharedMaterial = mapGenerator.allMapRegions[i]._material;
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

