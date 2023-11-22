using System;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] GameObject hexPrefab;
    public int mapWidht = 25;
    public int mapHeight = 25;
    [Tooltip("HexMesh radius*2 = 2r")] public float tile_X_Offset = 1.75f;
    [Tooltip("HexMesh radius = r")] public float xOffset; // = tile_X_Offset * .5f; 
    float tile_Z_Offset; // = Mathf.Sqrt(3) * tile_X_Offset * .5f;

    [Space]
    public List<Hex> HEXES = new();
    [Space]

    public List<GameObject> hexes_Objects = new();
    public List<MeshRenderer> hexes_Renderer = new();

    public Vector3 initScale;


    public Action OnGridCreate;

    public static HexGrid ins; // singleton
    void Awake()
    {
        ins = this;
        initScale = transform.localScale;
    }

    public void ClearAndCreateEmit(int _mapWidht, int _mapHeight)
    {
        mapWidht = _mapWidht;
        mapHeight = _mapHeight;
        ClearAndCreate();
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
            hexes_Objects.Clear();
            foreach (Transform hex in transform)
            {
                hexes_Objects.Add(hex.gameObject);
            }
        }
        foreach (var item in hexes_Objects)
        {
            DestroyImmediate(item);
        }
        hexes_Renderer.Clear();
        hexes_Objects.Clear();
    }


    void CreateHexGrid()
    {
        hexes_Renderer.Clear();
        hexes_Objects.Clear();
        HEXES.Clear();
        xOffset = tile_X_Offset * .5f;
        tile_Z_Offset = Mathf.Sqrt(3) * tile_X_Offset * .5f;
        int hex_id = 0;
        for (int x = 0; x < mapWidht; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                GameObject tempGo = Instantiate(hexPrefab);
                tempGo.transform.parent = transform;
                tempGo.name = "hex_" + x + "-" + z;
                hexes_Objects.Add(tempGo);
                hexes_Renderer.Add(tempGo.GetComponentInChildren<MeshRenderer>());
                if (z % 2 == 0)
                {
                    tempGo.transform.localPosition = new Vector3(x * tile_X_Offset, 0, z * tile_Z_Offset);
                }
                else
                {
                    tempGo.transform.localPosition = new(x * tile_X_Offset + xOffset, 0, z * tile_Z_Offset);
                }
                Hex tempHex = tempGo.GetComponentInChildren<Hex>();
                tempHex.SetHex(hex_id, initScale.x, true);
                hex_id++;
                HEXES.Add(tempHex);
            }
        }
        OnGridCreate?.Invoke();
    }


    public Vector2 HexesSize()
    {
        return new(
            (hexes_Objects[hexes_Objects.Count - 1].transform.position.x - hexes_Objects[0].transform.position.x),
            (hexes_Objects[hexes_Objects.Count - 1].transform.position.z - hexes_Objects[0].transform.position.z));
    }
}
