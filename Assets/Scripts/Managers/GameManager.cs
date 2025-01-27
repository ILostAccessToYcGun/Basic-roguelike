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

    public enum GameState { Gameplay, Paused, Main_Menu, Win, Dead}
    public GameState currentGameState;

    private int stageCount;


    public void IncrementStageCount()
    {
        stageCount++;
    }

    public void ResetStageCount()
    {
        stageCount = 0;
    }

    public int GetStageCount() {  return stageCount; }

    public void SetPlayer(Characters _player) { player = _player; }

    public void SetBuffManager(BuffManager bm) { buffManager = bm; }

    public void SetStageManager(StageManager sm) { stageManager = sm; }
    public void SetUIManager(UIManager uim) { uiManager = uim; }

    public void ChangeGameState(GameState gs)
    {
        //if (currentGameState == gs) return; 
        //add in some checks to see what was the previous game state and do the things you need to de between the previous and the new (gs)
        switch (currentGameState) //this is the game state we are leaving
        {
            case GameState.Gameplay:
                if (uiManager.EliminateEnemiesUI)
                    uiManager.EliminateEnemies_Toggle(false);
                if (uiManager.SurviveUI)
                    uiManager.Survive_Toggle(false);
                break;
            case GameState.Paused:
                uiManager.PauseMenu_Toggle(false);
                uiManager.PlayerStats_Toggle(false);
                Time.timeScale = 1f;
                break;
            case GameState.Main_Menu:
                //switch scene
                uiManager.MainMenu_Toggle(false);
                //player = FindAnyObjectByType<Characters>();
                //buffManager = FindAnyObjectByType<BuffManager>();
                //stageManager = FindAnyObjectByType<StageManager>();
                
                break;
            case GameState.Win:
                //huh
                break;
            case GameState.Dead:
                player.Heal(player.f_MaxHP);//bruh
                uiManager.GameOver_Toggle(false);
                Time.timeScale = 1f;
                //disable UI
                break;
        }

        switch (gs) //game state we are entering
        {
            case GameState.Gameplay:
                if (stageManager != null)
                {
                    if (stageManager.currentClrCon == StageManager.ClearCondition.Survive)
                        uiManager.Survive_Toggle(true);
                    else if (stageManager.currentClrCon == StageManager.ClearCondition.Eliminate)
                        uiManager.EliminateEnemies_Toggle(true);
                }
                
                break;
            case GameState.Paused:
                uiManager.PauseMenu_Toggle(true);
                Time.timeScale = 0f;
                break;
            case GameState.Main_Menu:
                //switch scene
                uiManager.MainMenu_Toggle(true);
                break;
            case GameState.Win:
                //huh
                break;
            case GameState.Dead:
                //show the UI
                Time.timeScale = 0f;
                uiManager.GameOver_Toggle(true);
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
