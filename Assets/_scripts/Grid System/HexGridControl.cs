using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridControl : MonoBehaviour
{
    [SerializeField] HexGridGenerator gridGenerator;
    [SerializeField] int mapWidht = 25;
    [SerializeField] int mapHeight = 25;

    private void OnValidate()
    {
        if (gridGenerator == null) gridGenerator = GetComponentInChildren<HexGridGenerator>();
    }

    [ContextMenu("Generate Grid")]
    void GridGenerator()
    {
        gridGenerator.ClearAndCreateEmit(mapWidht, mapHeight);
    }
}
