using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HexGridGenerator : MonoBehaviour
{
    [SerializeField] GameObject hexPrefab;
    public int mapWidht = 25;
    public int mapHeight = 25;
    [SerializeField] float tile_X_Offset = 1.75f;
    float tile_Z_Offset; // = Mathf.Sqrt(3) * tile_X_Offset * .5f;
    float xOffset; // = tile_X_Offset * .5f;

    public List<GameObject> hexes_Object = new();
    public List<MeshRenderer> hexes_Renderer = new();
    public static HexGridGenerator Instance;
    Vector3 initScale;


    public Action OnGridCreate;


    void Awake()
    {
        Instance = this;
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
            hexes_Object.Clear();
            foreach (Transform hex in transform)
            {
                hexes_Object.Add(hex.gameObject);
            }
        }
        foreach (var item in hexes_Object)
        {
            DestroyImmediate(item);
        }
        hexes_Renderer.Clear();
        hexes_Object.Clear();
    }


    void CreateHexGrid()
    {
        hexes_Renderer.Clear();
        hexes_Object.Clear();
        xOffset = tile_X_Offset * .5f;
        tile_Z_Offset = Mathf.Sqrt(3) * tile_X_Offset * .5f;
        for (int x = 0; x < mapWidht; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                GameObject tempGo = Instantiate(hexPrefab);
                tempGo.transform.parent = transform;
                tempGo.name = "hex_" + x + "-" + z;
                hexes_Object.Add(tempGo);
                hexes_Renderer.Add(tempGo.GetComponentInChildren<MeshRenderer>());
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
        OnGridCreate?.Invoke();
    }


    public Vector2 HexesSize()
    {
        return new(
            (hexes_Object[hexes_Object.Count - 1].transform.position.x - hexes_Object[0].transform.position.x),
            (hexes_Object[hexes_Object.Count - 1].transform.position.z - hexes_Object[0].transform.position.z));
    }
}
