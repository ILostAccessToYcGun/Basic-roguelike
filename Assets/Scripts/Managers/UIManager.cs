using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    //For all the UI Toggles, if you pass in a true variable, it will show the UI, false will Hide the UI
    //I want the managers to be singletons i think.
    private StageManager stageManager;
    private Characters player;
    

    private void Awake()
    {
        stageManager = FindAnyObjectByType<StageManager>();
        player = FindAnyObjectByType<Characters>();

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
    public void MainMenuToggle(bool turnOn)
    {
        MainMenuUI.SetActive(turnOn);
    } 

    public void ButtonQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    public void ButtonSettings()
    {
        Debug.Log("Settings");
        MainMenuToggle(false);
        SettingsToggle(true);
    }

    public void ButtonPlay()
    {
        Debug.Log("Play");
        //Change Game manager state
        //changeScene
    }

    #endregion

    #region PauseMenuUI
    public GameObject PauseMenuUI;
    public void PauseMenuToggle(bool turnOn)
    {
        PauseMenuUI.SetActive(turnOn);
    }

    public void ButtonResume()
    {
        Debug.Log("Resume");
        PauseMenuToggle(false);
        //change to appropriate game state
    }

    public void ButtonReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu");
        //change scene
    }

    public void ButtonPlayerStats()
    {
        Debug.Log("PlayerStats");
        PlayerStatsUIToggle(true);
        PauseMenuToggle(false);
        //TODO:change game state?
    }
    #endregion

    #region SettingsUI
    public GameObject SettingsUI;
    public void SettingsToggle(bool turnOn)
    {
        SettingsUI.SetActive(turnOn);
    }

    public void ButtonBack()
    {
        Debug.Log("Back");
        SettingsToggle(false);
        MainMenuToggle(true);
    }
    #endregion

    #region EliminateEnemiesUI
    public GameObject EliminateEnemiesUI;
    public TextMeshProUGUI eliminateCount;
    public void EliminateEnemiesUIToggle(bool turnOn)
    {
        EliminateEnemiesUI.SetActive(turnOn);
    }

    public void UpdateUIEnemyCounter()
    {
        eliminateCount.text = "Enemies Remaining: " + stageManager.enemiesAlive;
    }
    #endregion

    #region SurviveUI
    public GameObject SurviveUI;
    public TextMeshProUGUI surviveTimer;
    public void SurviveUIToggle(bool turnOn)
    {
        SurviveUI.SetActive(turnOn);
    }

    public void UpdateUISurviveTimer()
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
    public void StageCleared()
    {
        
        //tempPanelColor.a = 0f;
        //tempTextColor.a = 0f;
        //stageClearPanel.color = tempPanelColor;
        //stageClearText.color = tempTextColor;

        if (SurviveUI.activeSelf == true)
        {
            SurviveUIToggle(false);
        }
        if (EliminateEnemiesUI.activeSelf == true)
        {
            EliminateEnemiesUIToggle(false);
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
    public void PlayerStatsUIToggle(bool turnOn)
    {
        PlayerStatsUI.SetActive(turnOn);
    }
    
    public void UpdatePlayerStatsUI()
    {
        StatText.text = "HP: " + player.f_MaxHP + 
            "<br>ATK: " + player.f_ATK + 
            "<br>SPD: " + player.f_SPD + 
            "<br>DEF: " + player.f_DEF + 
            "<br>CD: " + player.f_CD + 
            "<br>Jumps: " + player.f_JUMP + 
            "<br>Jump Height: " + player.f_jHeight;
    }

    #endregion
}
