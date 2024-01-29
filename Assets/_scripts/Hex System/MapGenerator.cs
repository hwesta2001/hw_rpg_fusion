using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MapDisplay))]
public class MapGenerator : MonoBehaviour
{
    //Debug icin
    [Header("Component Chaches")]
    [SerializeField] HexToMap hexToMap;
    [SerializeField] MapDisplay display;
    [Header("Var")]
    [SerializeField] DrawMode drawMode;
    [SerializeField] int mapWidth;
    [SerializeField] int mapHeight;
    [SerializeField] float noiseScale;
    public int octaves;
    [Range(0, 1)]
    [SerializeField] float persistance;
    [SerializeField] float lacunarity;
    [SerializeField] Vector2 offset;
    [SerializeField] int seed;
    public bool randomSeed;
    [SerializeField] bool autoUpdate;
    [SerializeField] bool canHexMapChange;
    //------------------------------------------
    [Header("Regions")]
    [SerializeField] Material region_mat_base;
    [SerializeField] RegionType[] regions;
    [Header("-------------------------")]
    public List<RegionType> allMapRegions = new();


    public void GererateMapEmit(int _mapWidth, int _mapHeight)
    {
        mapWidth = _mapWidth;
        mapHeight = _mapHeight;
        GenerateMap();
    }

    void GenerateMap()
    {
        if (randomSeed) seed = UnityEngine.Random.Range(1, 1000); //debug için random seedler
        SetRegionMats();
        allMapRegions.Clear();
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        allMapRegions.Add(regions[i]);
                        break;
                    }
                }
            }
        }

        if (!display.gameObject.activeInHierarchy) return;
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }


    }

    void OnValidate()
    {
        if (!autoUpdate) return;
        //if (display == null) display = GetComponent<MapDisplay>();
        //if (hexToMap == null) hexToMap = GetComponent<HexToMap>();
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }

        if (autoUpdate)
        {
            if (canHexMapChange)
            {
                hexToMap.GenerateMap();
            }
            else
            {
                GenerateMap();
            }
        }
    }


    [ContextMenu("Set Regions Materials")]
    void SetRegionMats()
    {
        for (int i = 0; i < regions.Length; i++)
        {
            Material mat = new(region_mat_base)
            {
                name = regions[i].name + "_mat",
                color = regions[i].colour

            };
            regions[i]._material = mat;
        }
    }
}


[System.Serializable]
public struct RegionType
{
    public string name;
    public float height;
    public Color colour;
    public Material _material;
}

public enum DrawMode
{
    NoiseMap,
    ColourMap
}
