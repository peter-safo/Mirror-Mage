using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightRay : MonoBehaviour
{
    public float rayLength = 10f;
    public int maxBounces = 10; // Number of reflections allowed

    void Start()
    {
        StartCoroutine(ShootRaysRoutine()); // Start shooting rays in cycles
    }

    IEnumerator ShootRaysRoutine()
    {
        while (true) // Infinite loop to keep repeating
        {
            float startTime = Time.time;

            while (Time.time < startTime + 5f) // Shoot for 5 seconds
            {
                // Create a HashSet to track hit objects per cycle
                HashSet<Collider2D> hitObjects = new HashSet<Collider2D>();

                CastRay(transform.position, transform.right, maxBounces, 0, hitObjects);
                yield return new WaitForSeconds(0.2f); // Delay to control raycasting speed
            }

            yield return new WaitForSeconds(2f); // Wait 2 seconds before restarting
        }
    }

    void CastRay(Vector2 origin, Vector2 direction, int remainingBounces, int bounceCount, HashSet<Collider2D> hitObjects)
    {
        if (remainingBounces <= 0) return; // Stop if max bounces are reached

        int mirrorLayer = LayerMask.NameToLayer("Mirror"); // Mirror Layer
        int enemyLayer = LayerMask.NameToLayer("Enemy");   // Enemy Layer
        int layerMask = LayerMask.GetMask("Mirror", "Enemy", "Default"); // Add relevant layers

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 50f, layerMask);

        Debug.DrawRay(origin, direction * 50f, GetRayColor(bounceCount), 5f);
        Debug.Log($"Bounce {bounceCount}: Casting ray from {origin} in direction {direction}");

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.layer == enemyLayer)
            {
                Debug.Log("skeleton is getting slapped");

                Enemy enemyScript = hit.transform.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.health -= 1; // Deal damage
                }

                // Continue the ray by shifting origin slightly forward
                Vector2 newOrigin = hit.point + direction * 0.1f;
                CastRay(newOrigin, direction, remainingBounces, bounceCount, hitObjects);
            }
            else if (hitObject.layer == mirrorLayer && !hitObjects.Contains(hit.collider))
            {
                hitObjects.Add(hit.collider); // Avoid hitting the same mirror twice

                // Reflection logic
                Transform hitTransform = hit.collider.transform;
                float angle = hitTransform.eulerAngles.z;
                Debug.Log($"Bounce {bounceCount}: Hit Mirror {hit.collider.name} at {hit.point} with mirror angle {angle}");

                Vector2 reflectedDirection;

                if (Mathf.Approximately(angle, 270f))
                {
                    reflectedDirection = new Vector2(-direction.y, -direction.x).normalized;
                }
                else if (Mathf.Approximately(angle, 0f))
                {
                    reflectedDirection = new Vector2(direction.y, direction.x).normalized;
                }
                else if (Mathf.Approximately(angle, 180f))
                {
                    if (direction.x > 0 && Mathf.Abs(direction.y) < 0.1f)
                    {
                        reflectedDirection = Vector2.up;
                    }
                    else if (direction.y < 0 && Mathf.Abs(direction.x) < 0.1f)
                    {
                        reflectedDirection = Vector2.left;
                    }
                    else
                    {
                        reflectedDirection = Vector2.Reflect(direction, hit.normal).normalized;
                    }
                }
                else if (Mathf.Approximately(angle, 90f))
                {
                    if (direction.x < 0 && Mathf.Abs(direction.y) < 0.1f)
                    {
                        reflectedDirection = Vector2.up;
                    }
                    else if (direction.y < 0 && Mathf.Abs(direction.x) < 0.1f)
                    {
                        reflectedDirection = Vector2.right;
                    }
                    else
                    {
                        reflectedDirection = Vector2.Reflect(direction, hit.normal).normalized;
                    }
                }
                else
                {
                    reflectedDirection = Vector2.Reflect(direction, hit.normal).normalized;
                }

                // Get the exact center of the mirror tile
                Vector2 mirrorCenter = hit.collider.bounds.center;

                // Offset to the **edge** of the mirror (half of its 0.32 size)
                float mirrorHalfSize = 0.5f;
                Vector2 newOrigin = mirrorCenter + reflectedDirection * mirrorHalfSize; // Move to edge

                Debug.Log($"Bounce {bounceCount}: New ray from {newOrigin} in direction {reflectedDirection}");

                CastRay(newOrigin, reflectedDirection, remainingBounces - 1, bounceCount + 1, hitObjects);
            }
            else
            {
                Debug.Log($"Bounce {bounceCount}: Hit an unrecognized object {hit.collider.name}");
            }
        }
        else
        {
            Debug.Log($"Bounce {bounceCount}: No hit detected.");
        }
    }

    // Function to return a different color for each bounce
    Color GetRayColor(int bounce)
    {
        Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan };
        return colors[bounce % colors.Length]; // Loop through colors
    }
}