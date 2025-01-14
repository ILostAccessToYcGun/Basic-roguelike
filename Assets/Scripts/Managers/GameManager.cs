using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    Characters player;
    //EnemySpawner enemySpawner

    public enum GameState { In_Game, Paused, Main_Menu, Win, Lose}
    public GameState currentGameState;

    public int stageCount;


    public void IncrementStageCount()
    {
        stageCount++;
    }

    public void ResetStageCount()
    {
        stageCount = 0;
    }
    public void ChangeGameState(GameState gs)
    {
        //if (currentGameState == gs) return; 
        //add in some checks to see what was the previous game state and do the things you need to de between the previous and the new (gs)
        switch (currentGameState) //this is the game state we are leaving
        {
            case GameState.In_Game:
                uiManager.EliminateEnemiesUIToggle(false);
                uiManager.SurviveUIToggle(false);
                break;
            case GameState.Paused:
                uiManager.PauseMenuToggle(false);
                uiManager.PlayerStatsUIToggle(false);
                Time.timeScale = 1f;
                break;
            case GameState.Main_Menu:
                //switch scene
                uiManager.MainMenuToggle(false);
                break;
            case GameState.Win:
                //huh
                break;
            case GameState.Lose:
                //huh
                break;
        }

        switch (gs) //game state we are entering
        {
            case GameState.In_Game:
                if (stageManager.currentClrCon == StageManager.ClearCondition.Survive)
                {
                    uiManager.SurviveUIToggle(true);
                }
                else if (stageManager.currentClrCon == StageManager.ClearCondition.Eliminate)
                {
                    uiManager.EliminateEnemiesUIToggle(true);
                }
                break;
            case GameState.Paused:
                uiManager.PauseMenuToggle(true);
                Time.timeScale = 0f;
                break;
            case GameState.Main_Menu:
                //switch scene
                uiManager.MainMenuToggle(true);
                break;
            case GameState.Win:
                //huh
                break;
            case GameState.Lose:
                //huh
                break;
        }

        currentGameState = gs;
    }
    private void Awake()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
        stageManager = FindAnyObjectByType<StageManager>();
        uiManager = FindAnyObjectByType<UIManager>();
        player = FindAnyObjectByType<Characters>();

        stageCount = 0;
        ChangeGameState(GameState.Main_Menu);
    }
}
