using UnityEngine;

public class HexToMap : MonoBehaviour

{
    [SerializeField] bool canChangeHexMaterial;
    [SerializeField] HexGrid hexGrid;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] Vector2 hexesSize;
    [SerializeField] Transform mapPlane;

    [ContextMenu("OnGridCreateReset")]
    void OnGridCreateReset()
    {
        //gridGenerator.OnGridCreate = GenerateMap;
        hexGrid.OnGridCreate = null;
        hexGrid.OnGridCreate += GenerateMap;
    }
    private void OnValidate()
    {
        hexGrid.OnGridCreate = GenerateMap;
    }
    void OnDisable()
    {
        hexGrid.OnGridCreate -= GenerateMap;
    }

    public void GenerateMap()
    {
        ChangeSize();
        mapGenerator.GererateMapEmit(hexGrid.mapWidht, hexGrid.mapHeight);
        if (!canChangeHexMaterial) return;
        ScaleHexes();
        ColorHexes();
        HexMoveablesSetter();
    }

    public void ChangeSize()
    {
        hexesSize = hexGrid.HexesSize();
        mapPlane.localScale = new(hexesSize.x * .1f, 1, hexesSize.y * .1f);
    }

    void HexMoveablesSetter()
    {
        for (int i = 0; i < hexGrid.HEXES.Count; i++)
        {
            if (mapGenerator.allMapRegions[i].height >= .8999f)
            {
                hexGrid.HEXES[i].moveable = false;
            }
            else if (mapGenerator.allMapRegions[i].height <= .2001f)
            {
                hexGrid.HEXES[i].moveable = false;
            }
            else
            {
                hexGrid.HEXES[i].moveable = true;
            }
        }
    }

    void ScaleHexes()
    {
        float hightOffsetMax = 25f; // snow ile dað arasýndaki yükseklik farkýdýr. must be bigger than 10
        float hightMin = 30f; // daðýn min yüksekliðidir.
        foreach (var item in hexGrid.hexes_Objects)
        {
            item.transform.localScale = Vector3.one;
        }
        for (int i = 0; i < hexGrid.hexes_Objects.Count; i++)
        {
            if (mapGenerator.allMapRegions[i].height >= .8999f)
            {
                hexGrid.hexes_Objects[i].transform.localScale = new(1, -(10 * hightOffsetMax * .9f - hightMin) + 10 * hightOffsetMax * mapGenerator.allMapRegions[i].height, 1);
            }
        }
    }


    void ColorHexes()
    {
        for (int i = 0; i < hexGrid.hexes_Renderer.Count; i++)
        {
            hexGrid.hexes_Renderer[i].sharedMaterial = mapGenerator.allMapRegions[i]._material;
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

