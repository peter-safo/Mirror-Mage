using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MirrorSelector : MonoBehaviour
{
    public Tilemap mainTilemap;
    private bool Mirrorplaced;
    public GameObject mirrorPrefab;
    public LayerMask mirrorMask;
    private Transform selectedMirror;
    private List<GameObject> mirrors = new List<GameObject>(); // List to store mirrors
    // Update is called once per frame


    public void DestroyMirros()
    {
        Invoke(nameof(DeleteMirrors), 1f);
    }
    private void DeleteMirrors()
    {
        foreach (GameObject mirror in mirrors)
        {
            Destroy(mirror);
        }
        mirrors.Clear(); // Clear the list after deleting all mirrors
    }
    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = mainTilemap.WorldToCell(worldPos);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);
            if (hitCollider != null)
            {
                selectedMirror = hitCollider.transform; // Store the selected mirror

                if (((1 << hitCollider.gameObject.layer) & mirrorMask) != 0)
                {
                    Debug.Log("Clicked on an object in the 'Mirror' layer.");
                }
            }
            else
            {
                Vector3 cellCenterPos = mainTilemap.GetCellCenterWorld(cellPos);
                GameObject newMirror = Instantiate(mirrorPrefab, cellCenterPos + new Vector3(0.0f, 0.0f, -3.0f), Quaternion.identity);
                mirrors.Add(newMirror); // Add the new mirror to the list
            }

        }
        if (selectedMirror != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateMirror(90); // Rotate counterclockwise
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RotateMirror(-90); // Rotate clockwise
            }
        }

    }

    void RotateMirror(int angle)
    {
        float newRotation = selectedMirror.eulerAngles.z + angle;

        // Ensure the rotation is always within 0, 90, 180, 270
        newRotation = (newRotation + 360) % 360;

        if (Mathf.Approximately(newRotation, 360)) newRotation = 0; // Edge case: 360° should be 0°

        // Explicitly apply Euler angles instead of using Quaternion to avoid -180 in Inspector
        Vector3 correctedRotation = new Vector3(0, 0, newRotation);
        selectedMirror.eulerAngles = correctedRotation;

        Debug.Log("New Rotation: " + selectedMirror.eulerAngles.z);
    }

}