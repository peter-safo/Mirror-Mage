using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Tilemap baseTilemap;

    //[SerializeField] private Transform cam;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector3Int tilemapOrigin = baseTilemap.origin;

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector3 worldPos = baseTilemap.GetCellCenterWorld(new Vector3Int(x + tilemapOrigin.x, y + tilemapOrigin.y, tilemapOrigin.z));

                var spawnedTile = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }

        //cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
