using UnityEngine;

public class HexGridControl : MonoBehaviour
{
    [SerializeField] HexGrid gridGenerator;
    [SerializeField] int mapWidht = 25;
    [SerializeField] int mapHeight = 25;

    private void OnValidate()
    {
        if (gridGenerator == null) gridGenerator = GetComponentInChildren<HexGrid>();
    }

    [ContextMenu("Generate Grid")]
    void GridGenerator()
    {
        gridGenerator.ClearAndCreateEmit(mapWidht, mapHeight);
    }
}
