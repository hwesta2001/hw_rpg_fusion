using UnityEngine;
using System.Collections;
using static UnityEditor.PlayerSettings.SplashScreen;
using UnityEditor.AssetImporters;
using System.Collections.Generic;

[RequireComponent(typeof(MapDisplay))]
public class MapGenerator : MonoBehaviour
{
    [SerializeField] MapDisplay display;
    [SerializeField] DrawMode drawMode;
    [SerializeField] int mapWidth;
    [SerializeField] int mapHeight;
    [SerializeField] float noiseScale;
    public int octaves;
    [Range(0, 1)]
    [SerializeField] float persistance;
    [SerializeField] float lacunarity;
    [SerializeField] int seed;
    [SerializeField] Vector2 offset;
    [SerializeField] bool autoUpdate;
    [SerializeField] TerrainType[] regions;

    public List<TerrainType> allNoises = new();

    //Debug icin
    HexToMap hexToMap;


    public void GererateMapEmit(int _mapWidth, int _mapHeight)
    {
        mapWidth = _mapWidth;
        mapHeight = _mapHeight;
        GenerateMap();
    }

    void GenerateMap()
    {
        allNoises.Clear();
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
                        allNoises.Add(regions[i]);
                        break;
                    }
                }
                //allNoises.Add(noiseMap[x, y]);
            }
        }

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
        if (display == null) display = GetComponent<MapDisplay>();
        if (hexToMap == null) hexToMap = GetComponent<HexToMap>();
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
        if (autoUpdate) GenerateMap();
        hexToMap.ChangeSize();
        hexToMap.GenerateMap();

    }

}


[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}

public enum DrawMode
{
    NoiseMap,
    ColourMap
}
