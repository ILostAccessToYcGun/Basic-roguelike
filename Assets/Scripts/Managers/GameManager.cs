using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }



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
                if (uiManager.EliminateEnemiesUI)
                    uiManager.EliminateEnemiesUIToggle(false);
                if (uiManager.SurviveUI)
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
        DontDestroyOnLoad(gameObject);

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
        }

        uiManager = FindAnyObjectByType<UIManager>();
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("TitleScreen"))
        {
            buffManager = FindAnyObjectByType<BuffManager>();
            stageManager = FindAnyObjectByType<StageManager>();
            player = FindAnyObjectByType<Characters>();
        }
        else
        {
            stageCount = 0;
            ChangeGameState(GameState.Main_Menu);
            Debug.Log("Mainmenu");
        }
        

        
    }
}   
