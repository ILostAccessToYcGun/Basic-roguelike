using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    //For all the UI Toggles, if you pass in a true variable, it will show the UI, false will Hide the UI
    //I want the managers to be singletons i think.
    private StageManager stageManager;
    private Characters player;
    private GameManager gameManager;
    private RunStatisticsManager rsManager;
    
    

    private void Awake()
    {
        stageManager = FindAnyObjectByType<StageManager>();
        player = FindAnyObjectByType<Characters>();
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.SetUIManager(this);
        rsManager = FindAnyObjectByType<RunStatisticsManager>();


        if (StageClearUI != null )
        {
            //stageClearPanel = StageClearUI.GetComponentInChildren<Image>();
            //stageClearText = StageClearUI.GetComponentInChildren<TextMeshProUGUI>();

            //tempPanelColor = stageClearPanel.color;
            //tempTextColor = stageClearText.color;

            fadeBool = false;
            //hasFadedin = false;
        }

        //hmmmmm
        //if (PlayerStatsUI != null )
        //{
        //    HPText = get;
        //    ATKText;
        //    SPDText;
        //    DEFText;
        //    CDText;
        //    JUMPText;
        //    jHeightText;
        //}
    }

    private void Update()
    {
        if (fadeBool)
        {
            fadeTimer -= Time.deltaTime;

            if (fadeTimer < 0 )
            {
                StageClearUI.SetActive(false);
                fadeBool = false;
            }
            //if (hasFadedin)
            //{
            //    StageClearFadeOut();
            //    Debug.Log("Fade out");
            //}
            //else
            //{
            //    StageClearFadeIn();
            //    Debug.Log("Fade in");
            //}

        }
    }

    #region MainMenuUI
    public GameObject MainMenuUI;
    public void MainMenu_Toggle(bool turnOn)
    {
        MainMenuUI.SetActive(turnOn);
    } 

    public void MainMenu_ButtonQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    public void MainMenu_ButtonSettings()
    {
        Debug.Log("Settings");
        MainMenu_Toggle(false);
        Settings_Toggle(true);
    }

    public void MainMenu_ButtonPlay()
    {
        Debug.Log("Play");
        
        SceneManager.LoadScene("TestStage", LoadSceneMode.Single);
        gameManager.ChangeGameState(GameManager.GameState.Gameplay);
        //changeScene
    }

    #endregion

    #region PauseMenuUI
    public GameObject PauseMenuUI;
    public void PauseMenu_Toggle(bool turnOn)
    {
        PauseMenuUI.SetActive(turnOn);
    }

    public void PauseMenu_ButtonResume()
    {
        Debug.Log("Resume");
        gameManager.ChangeGameState(GameManager.GameState.Gameplay);
    }

    public void PauseMenu_ButtonReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu");
        gameManager.ChangeGameState(GameManager.GameState.Main_Menu);
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }

    public void PauseMenu_ButtonPlayerStats()
    {
        Debug.Log("PlayerStats");
        PlayerStats_Toggle(true);
        PauseMenu_Toggle(false);
        //no change game state, player stats will be part of the pause game state
        //TODO: maybe just move the camera to be centered on the player
    }
    #endregion

    #region SettingsUI
    public GameObject SettingsUI;
    public void Settings_Toggle(bool turnOn)
    {
        SettingsUI.SetActive(turnOn);
    }

    public void Settings_ButtonBack()
    {
        Debug.Log("Back");
        Settings_Toggle(false);
        MainMenu_Toggle(true);
    }
    #endregion

    #region EliminateEnemiesUI
    public GameObject EliminateEnemiesUI;
    public TextMeshProUGUI eliminateCount;
    public void EliminateEnemies_Toggle(bool turnOn)
    {
        EliminateEnemiesUI.SetActive(turnOn);
    }

    public void EliminateEnemies_UpdateUIEnemyCounter()
    {
        eliminateCount.text = "Enemies Remaining: " + stageManager.enemiesAlive;
    }
    #endregion

    #region SurviveUI
    public GameObject SurviveUI;
    public TextMeshProUGUI surviveTimer;
    public void Survive_Toggle(bool turnOn)
    {
        SurviveUI.SetActive(turnOn);
    }

    public void Survive_UpdateUISurviveTimer()
    {
        surviveTimer.text = "Time Remaining: " + Mathf.Floor(stageManager.timeToSurvive * 10) / 10;
    }
    #endregion

    #region StageClearUI
    public GameObject StageClearUI;
    //private Image stageClearPanel;
    //private TextMeshProUGUI stageClearText;
    //private Color tempPanelColor;
    //private Color tempTextColor;
    private bool fadeBool;
    private float fadeTimer;
    //private bool hasFadedin;
    public void StageClear_StageCleared()
    {
        
        //tempPanelColor.a = 0f;
        //tempTextColor.a = 0f;
        //stageClearPanel.color = tempPanelColor;
        //stageClearText.color = tempTextColor;

        if (SurviveUI.activeSelf == true)
        {
            Survive_Toggle(false);
        }
        if (EliminateEnemiesUI.activeSelf == true)
        {
            EliminateEnemies_Toggle(false);
        }

        StageClearUI.SetActive(true);
        fadeTimer = 3;
        fadeBool = true;


    }

    //Fading in and out doesnt work right now. but thats not a priority, so ill leave it for later
    //TODO: fix the fade so that it works

    //public void StageClearFadeIn()
    //{ 
    //    tempPanelColor.a = tempPanelColor.a + 0.01f;
    //    tempTextColor.a = tempTextColor.a + 0.01f;
    //
    //    //Debug.Log(tempPanelColor.a);
    //    //Debug.Log(tempTextColor.a);
    //
    //    if (tempPanelColor.a == 1 && tempTextColor.a == 1)
    //    {
    //        hasFadedin = true;
    //    }
    //}

    //public void StageClearFadeOut()
    //{
    //    tempPanelColor.a = Mathf.Lerp(1, 0, 0.01f);
    //    tempTextColor.a = Mathf.Lerp(1, 0, 0.01f);
    //
    //    if (tempPanelColor.a == 0 && tempTextColor.a == 0)
    //    {
    //        StageClearUI.SetActive(false);
    //        fadeBool = false;
    //        hasFadedin = false;
    //    }
    //    
    //}

    #endregion

    #region PlayerStatsUI
    public GameObject PlayerStatsUI;
    public TextMeshProUGUI StatText;
    public void PlayerStats_Toggle(bool turnOn)
    {
        PlayerStatsUI.SetActive(turnOn);
    }
    
    public void PlayerStats_UpdatePlayerStatsUI()
    {
        StatText.text = "HP: " + player.f_MaxHP +  // i think the issyue nulkl ref is for the player
            "<br>ATK: " + player.f_ATK + 
            "<br>SPD: " + player.f_SPD + 
            "<br>DEF: " + player.f_DEF + 
            "<br>CD: " + player.f_ATKSPD + 
            "<br>Jumps: " + player.f_JUMP + 
            "<br>Jump Height: " + player.f_REG;
    }

    #endregion

    #region GameOverUI
    public GameObject GameOverUI;
    public TextMeshProUGUI RunStatisticsText;

    public void GameOver_Toggle(bool turnOn)
    {
        GameOverUI.SetActive(turnOn);
    }

    public void GameOver_ButtonReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu");
        gameManager.ChangeGameState(GameManager.GameState.Main_Menu);
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }

    public void GameOver_Retry()
    {
        Debug.Log("Retry");
        gameManager.ChangeGameState(GameManager.GameState.Gameplay);
        SceneManager.LoadScene("TestStage", LoadSceneMode.Single);

    }

    public void UpdateRunStatistics()
    {
        RunStatisticsText.text = "Character: " + rsManager.GetCharacterType() +
            "\nStages Cleared: " + gameManager.GetStageCount() +
            "\nRun Time: " + (Mathf.Floor(rsManager.GetRunTime() * 100f)/ 100f) + "s" +
            "\nEnemies Killed: " + rsManager.GetTotalEnemiesKilled() +
            "\nDamage Dealt: " + rsManager.GetTotalDamageDealt() +
            "\nDamage Recieved: " + rsManager.GetTotalDamageRecieved() +
            "\nDamage Healed: " + rsManager.GetTotalDamageHealed() +
            "\nBuffs Consumed: " + rsManager.GetTotalBuffsConsumed() +
            "\nChance Points Allocated: " + rsManager.GetTotalChancePointsAllocated();
    }

    #endregion
}
