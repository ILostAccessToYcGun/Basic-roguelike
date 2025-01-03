using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    //For all the UI Toggles, if you pass in a true variable, it will show the UI, false will Hide the UI
    //I want the managers to be singletons i think.
    public StageManager stageManager;

    private void Awake()
    {
        stageManager = FindAnyObjectByType<StageManager>();
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



}
