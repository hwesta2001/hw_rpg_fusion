using UnityEngine;

public class HexGridControl : MonoBehaviour
{
    [SerializeField] HexGrid gridGenerator;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] bool RandomSeed;

    [SerializeField] int mapWidht = 25;
    [SerializeField] int mapHeight = 25;

    private void OnValidate()
    {
        if (gridGenerator == null) gridGenerator = GetComponentInChildren<HexGrid>();
        if (mapGenerator == null) mapGenerator = GetComponentInChildren<MapGenerator>();
        mapGenerator.randomSeed = RandomSeed;
    }

    [ContextMenu("Generate Grid")]
    void GridGenerator()
    {
        gridGenerator.ClearAndCreateEmit(mapWidht, mapHeight);
    }
}
