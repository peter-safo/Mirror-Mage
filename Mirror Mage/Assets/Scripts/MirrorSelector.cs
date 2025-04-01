using UnityEngine;

public class MirrorSelector : MonoBehaviour
{
    public LayerMask mirrorLayer; // Assign the "Mirror" layer in the Inspector
    private Transform selectedMirror;

    void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPos, mirrorLayer);

            if (hitCollider != null)
            {
                selectedMirror = hitCollider.transform; // Store the selected mirror
                Debug.Log("Selected Mirror: " + selectedMirror.name);
            }
        }

        // Rotate selected mirror
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