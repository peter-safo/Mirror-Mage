using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        Debug.Log(health);
    }
}
