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

    public void SetGameState(GameState gs)
    {
        switch (gs)
        {
            case GameState.In_Game:
                currentGameState = GameState.In_Game;
                uiManager.PauseMenuToggle(false);
                break;
            case GameState.Paused:
                currentGameState = GameState.Paused;
                uiManager.PauseMenuToggle(true);
                break;
            case GameState.Main_Menu:
                currentGameState = GameState.Main_Menu;
                //switch scene
                uiManager.MainMenuToggle(true);
                break;
            case GameState.Win:
                currentGameState = GameState.Win;
                //huh
                break;
            case GameState.Lose:
                currentGameState = GameState.Lose;
                //huh
                break;
        }
    }
    private void Awake()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
        stageManager = FindAnyObjectByType<StageManager>();
        uiManager = FindAnyObjectByType<UIManager>();
        SetGameState(GameState.Main_Menu);
    }
}
