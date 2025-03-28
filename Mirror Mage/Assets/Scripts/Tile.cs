using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
