using System.Xml.Serialization;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public TurnState currentTurn = TurnState.PlayerTurn;

    public KeyCode endTurnKey = KeyCode.Space;
    public float enemyMoveDelay = 0.5f;

    void Update()
    {
        if (currentTurn == TurnState.PlayerTurn)
        {
            if (Input.GetKeyDown(endTurnKey))
            {
                EndPlayerTurn();
            }
        }
    }

    void EndPlayerTurn()
    {
        currentTurn = TurnState.EnemyTurn;
        Invoke("StartEnemyTurn", enemyMoveDelay);
        Debug.Log("Enemy turn");
    }

    void StartEnemyTurn()
    {
        // Move all enemies
        Skeleton[] enemies = FindObjectsOfType<Skeleton>();
        foreach (Skeleton enemy in enemies)
        {
            enemy.MoveLeft();
        }

        // Return to player turn
        Invoke("StartPlayerTurn", enemyMoveDelay);
    }

    void StartPlayerTurn()
    {
        currentTurn = TurnState.PlayerTurn;
        Debug.Log("Player's turn");
    }
}
