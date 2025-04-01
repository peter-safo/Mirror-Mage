using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign an enemy prefab in the Inspector
    public Transform[] spawnPoints; // Array of multiple spawn points
    public float moveDistance = 1f; // Distance to move
    public float moveSpeed = 5f; // Speed of movement

    private List<GameObject> enemies = new List<GameObject>(); // Dynamic list of enemies

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Move all enemies when pressing Space
        {
            MoveEnemies();
        }

        if (Input.GetKeyDown(KeyCode.E)) // Press "E" to spawn a new enemy
        {
            SpawnEnemy();
        }
    }

    void MoveEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            StartCoroutine(MoveEnemy(enemy, moveDistance));
        }
    }

    System.Collections.IEnumerator MoveEnemy(GameObject enemy, float distance)
    {
        Vector3 startPosition = enemy.transform.position;
        Vector3 targetPosition = startPosition + Vector3.left * distance; // Moving left
        float elapsedTime = 0f;

        while (elapsedTime < (distance / moveSpeed))
        {
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (distance / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.transform.position = targetPosition; // Ensure exact position
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Pick a random spawn point
        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
        enemies.Add(newEnemy); // Add the new enemy to the list
    }
}
