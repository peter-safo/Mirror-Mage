using System.Xml.Serialization;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public TurnState currentTurn = TurnState.PlayerTurn;

    public KeyCode endTurnKey = KeyCode.Space;
    public float enemyMoveDelay = 0.5f;

    public GameObject lightController;
    public GameObject enemyController;
    public GameObject mirrorController;

    public void EndTurn()
    {
        EndPlayerTurn();
    }

    void EndPlayerTurn()
    {
        currentTurn = TurnState.EnemyTurn;
        Invoke("StartEnemyTurn", enemyMoveDelay);
        Debug.Log("Enemy turn");
    }

    void StartEnemyTurn()
    {
        // fire light ray
        lightController.GetComponent<LightRay>().ShootRay();

        //delete mirrors

        mirrorController.GetComponent<MirrorSelector>().DeleteMirrors();
        //spawn more enemies
        enemyController.GetComponent<EnemyManager>().SpawnEnemy();

        // Move all enemies
        enemyController.GetComponent<EnemyManager>().MoveEnemies();

        // Return to player turn
        Invoke("StartPlayerTurn", enemyMoveDelay);
    }

    void StartPlayerTurn()
    {
        currentTurn = TurnState.PlayerTurn;
        Debug.Log("Player's turn");
    }
}
