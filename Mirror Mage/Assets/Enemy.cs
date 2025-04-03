using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public Wizard _wizard;
    private EnemyManager enemyManager; // Reference to the EnemyManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
        _wizard = FindAnyObjectByType<Wizard>();
        enemyManager = FindObjectOfType<EnemyManager>(); // Get reference to EnemyManager
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("RayPlacement"));
        if (hit)
        {
            Debug.Log("Enemy is inside player collider!");
            _wizard.TakeDamage(20);

            if (enemyManager != null)
            {
                enemyManager.RemoveEnemy(gameObject); // Remove from list before deleting
            }

            Destroy(gameObject); // Deletes the enemy
        }
    }

 
}
