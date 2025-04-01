using UnityEngine;
using UnityEngine.Tilemaps;

public class Skeleton : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveSpeed = 0.5f;

    public void MoveLeft()
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + Vector3Int.left;
        Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

        // Force move (comment out after testing)
        transform.position = targetPosition;
        Debug.Log($"FORCE MOVED to {targetPosition}");
        //Debug.Log($"Enemy {name} attempting to move");

        //Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        //Vector3Int targetCell = currentCell + Vector3Int.left;

        //Debug.Log($"Current cell: {currentCell}, Target cell: {targetCell}");

        //// Debug draw the target cell
        //Vector3 worldPos = tilemap.GetCellCenterWorld(targetCell);
        //Debug.DrawLine(worldPos - Vector3.one * 0.5f, worldPos + Vector3.one * 0.5f, Color.red, 2f);

        //if (IsCellEmpty(targetCell))
        //{
        //    Debug.Log("Cell is empty - moving");
        //    Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);
        //    StartCoroutine(SmoothMove(targetPosition));
        //}
        //else
        //{
        //    Debug.Log("Cell is blocked - can't move");
        //}
    }

    bool IsCellEmpty(Vector3Int cellPosition)
    {
        Vector3 worldPos = tilemap.GetCellCenterWorld(cellPosition);

        // 1. Check tilemap collider
        if (tilemap.GetColliderType(cellPosition) != Tile.ColliderType.None)
        {
            Debug.Log($"Tile at {cellPosition} has collider");
            return false;
        }

        // 2. Check other colliders (ignore enemies and player)
        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if (hit != null)
        {
            Debug.Log($"Collider detected: {hit.gameObject.name}");
            // Ignore if it's our own collider or other enemies
            if (hit.gameObject == gameObject || hit.GetComponent<Enemy>() != null)
            {
                return true;
            }
            return false;
        }

        return true;
    }

    System.Collections.IEnumerator SmoothMove(Vector3 targetPosition)
    {
        Debug.Log($"Moving from {transform.position} to {targetPosition}");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < moveSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Debug.Log("Move complete");
    }
}
