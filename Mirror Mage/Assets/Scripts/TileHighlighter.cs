using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    public Tilemap mainTilemap;
    public Tilemap highlightTilemap;
    public GameObject highlightTile;

    private Vector3Int previousCell;
    private GameObject currentHighlight;
    private bool previousCellHighlighted;

    private void Update()
    {
        // Get mouse position in world space
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        // Convert to cell position
        Vector3Int cellPos = mainTilemap.WorldToCell(worldPos);

        // Check if mouse is over a different cell
        if (cellPos != previousCell)
        {
            // Clear previous highlight
            if (currentHighlight != null)
            {
                Destroy(currentHighlight);
                previousCellHighlighted = false;
            }

            // Check if new cell has a tile
            if (mainTilemap.HasTile(cellPos))
            {
                // Get center position of the cell
                Vector3 cellCenterPos = mainTilemap.GetCellCenterWorld(cellPos);

                // Instantiate highlight prefab
                currentHighlight = Instantiate(highlightTile, cellCenterPos + new Vector3(0.0f, 0.0f, -3.0f), Quaternion.identity);
                previousCellHighlighted = true;
                Debug.Log("Highlighted");
            }

            previousCell = cellPos;
        }
    }

    private void OnDisable()
    {
        // Clean up when script is disabled
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }
    }
}
