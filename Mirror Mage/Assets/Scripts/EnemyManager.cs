using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign an enemy prefab in the Inspector
    public Transform[] spawnPoints; // Array of multiple spawn points
    public float moveSpeed = 5f; // Speed of movement
    public LayerMask enemyLayer; // Layer for enemies to detect collisions
    public float checkRadius = 0.5f; // Radius to check for other enemies

    private List<GameObject> enemies = new List<GameObject>(); // Dynamic list of enemies
    private float[] possibleDistances = { 1f, 2f, 3f }; // Possible movement distances

    void Update()
    {
        // Uncomment these if you want to trigger movement or spawning via key press
        // if (Input.GetKeyDown(KeyCode.Space)) // Move all enemies when pressing Space
        // {
        //     MoveEnemies();
        // }

        // if (Input.GetKeyDown(KeyCode.E)) // Press "E" to spawn a new enemy
        // {
        //     SpawnEnemy();
        // }
    }

    public void MoveEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            float randomDistance = possibleDistances[Random.Range(0, possibleDistances.Length)]; // Choose a random distance
            StartCoroutine(MoveEnemy(enemy, randomDistance));
        }
    }

    IEnumerator MoveEnemy(GameObject enemy, float distance)
    {
        Vector3 startPosition = enemy.transform.position;
        Vector3 targetPosition = startPosition + Vector3.left * distance; // Moving left

        float elapsedTime = 0f;
        float moveDuration = distance / moveSpeed; // Time needed to complete movement

        while (elapsedTime < moveDuration)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.transform.position = targetPosition; // Ensure exact position
    }

    public void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        int enemyCount = Random.Range(1, 4); // Randomly choose between 1 and 3 enemies to spawn

        for (int i = 0; i < enemyCount; i++)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Pick a random spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
            enemies.Add(newEnemy); // Add the new enemy to the list
        }
    }
}
