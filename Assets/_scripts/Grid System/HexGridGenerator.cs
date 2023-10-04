using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    [SerializeField] GameObject hexPrefab;
    [SerializeField] int mapWidht = 25;
    [SerializeField] int mapHeight = 25;
    [SerializeField] float tile_X_Offset = 1.75f;
    float tile_Z_Offset; // = Mathf.Sqrt(3) * tile_X_Offset * .5f;
    float xOffset; // = tile_X_Offset * .5f;

    public List<GameObject> hexes = new();
    public static HexGridGenerator Instance;
    Vector3 initScale;
    void Awake()
    {
        Instance = this;
        initScale = transform.localScale;
    }


    [ContextMenu("ClearAndCreate")]
    void ClearAndCreate()
    {
        initScale = transform.localScale;
        transform.localScale = Vector3.one;
        ClearHexes();
        CreateHexGrid();
        transform.localScale = initScale;
    }


    [ContextMenu("ClearHexes")]
    void ClearHexes()
    {
        if (transform.childCount > 0)
        {
            hexes.Clear();
            foreach (Transform hex in transform)
            {
                hexes.Add(hex.gameObject);
            }
        }
        foreach (var item in hexes)
        {
            DestroyImmediate(item);
        }
        hexes.Clear();
    }


    void CreateHexGrid()
    {
        hexes.Clear();
        xOffset = tile_X_Offset * .5f;
        tile_Z_Offset = Mathf.Sqrt(3) * tile_X_Offset * .5f;
        for (int x = 0; x < mapWidht; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                GameObject tempGo = Instantiate(hexPrefab);
                tempGo.transform.parent = transform;
                tempGo.name = "hex_" + x + "-" + z;
                hexes.Add(tempGo);
                if (z % 2 == 0)
                {
                    tempGo.transform.localPosition = new Vector3(x * tile_X_Offset, 0, z * tile_Z_Offset);
                }
                else
                {
                    tempGo.transform.localPosition = new(x * tile_X_Offset + xOffset, 0, z * tile_Z_Offset);
                }
            }
        }
    }
}
