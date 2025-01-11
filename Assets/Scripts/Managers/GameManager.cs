using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Stage Count
    //Game States(In-Game, Pasued, Main Menu, Win, Lose)
    //Other managers, Buff manager, Stage Managers, UI Manager, Enemy Spawner(technically a manager)

    BuffManager buffManager;
    StageManager stageManager;
    UIManager uiManager;
    //EnemySpawner enemySpawner

    public enum GameState { In_Game, Paused, Main_Menu, Win, Lose}
    public GameState currentGameState;

    public int stageCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
