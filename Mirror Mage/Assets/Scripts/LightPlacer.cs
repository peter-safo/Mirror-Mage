using UnityEngine;
using UnityEngine.Tilemaps;

public class LightPlacer : MonoBehaviour
{
    public Tilemap mainTilemap;
    public GameObject LightSource;
    public LayerMask lightPlacement;

    private GameObject newLightsource; // Stores the active light source

    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0; // Ensure it's in the correct plane
        Vector3Int cellPos = mainTilemap.WorldToCell(worldPos);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPos, lightPlacement);

            if (hitCollider != null) // Check if clicking a valid placement area
            {
                if (newLightsource != null) // If a light source already exists, delete it
                {
                    Destroy(newLightsource);
                }

                Vector3 cellCenterPos = mainTilemap.GetCellCenterWorld(cellPos);
                newLightsource = Instantiate(LightSource, cellCenterPos + new Vector3(0.0f, 0.0f, -3.0f), Quaternion.identity);

                Debug.Log("Placed new light source.");
            }
        }
    }
}
