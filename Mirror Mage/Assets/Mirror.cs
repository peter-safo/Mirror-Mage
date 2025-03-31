using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject origin;
    public float rayLength = 10f;
    BoxCollider2D col;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];

        // Cast a ray inside this collider to check if it was hit
        if (col.Raycast(Vector2.down, hits, 10f) > 0) // Adjust direction if needed
        {
            Debug.Log(gameObject.name + " was hit by a ray!");
        }
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    RaycastHit2D newRay = Physics2D.Raycast(origin.transform.position, Vector2.right, rayLength);
    //    Debug.Log("has been hit ");

    //    if (newRay.collider != null)
    //    {
    //        Debug.Log("Hit object: " + hit.collider.name);
    //    }
    //}
    

}
