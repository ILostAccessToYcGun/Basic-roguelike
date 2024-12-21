using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    /*
    This is the way I want the 2 different clear conditions to work.
    ELIMINATE:
    The game will randomly generate a reasonable number of enemies to spawn ONCE,
    when the number of enemies alive is 0, we clear

    Survive:
    The game will randomly generate a reasonable number and HALF it for the number of enemies to spawn. If an enemy is killed in that time, another one spawns to replace it
    if we are surviving and time > 0 and enemiesalive is not equal to the target number ,spawn an enemy
    when the time reaches 0, _kill_all_enemies_ and we clear (not sure about killing enemies or making the player do that...)
    */

    public int enemiesAlive;
    public float timeToSurvive;
    public bool isStageCleared;

    public int targetEnemyCount;
    private EnemySpawner spawner;
    //public float stageSize;
    public enum ClearCondition { Survive, Eliminate };
    public ClearCondition currentClrCon;

    public void UpdateEnemyCount(int update)
    {
        enemiesAlive += update;
        CheckClearCondition();
    }

    private void CheckClearCondition()
    {
        if (!isStageCleared)
        {
            switch (currentClrCon)
            {
                case ClearCondition.Survive:
                    if (timeToSurvive <= 0)
                    {
                        Debug.Log("YOU HAVE SURVIVED");
                        //Kill all enemies
                        EndStage();
                    }
                    break;
                case ClearCondition.Eliminate:
                    if (enemiesAlive <= 0)
                    {
                        Debug.Log("YOU HAVE KILLED ALL ENEMIES");
                        EndStage();
                    }
                    break;
            }
        }
    }

    private void BeginStage()
    {
        int random = Random.Range(0, 2);
        currentClrCon = (ClearCondition)random;

        int randomEnemyCount = Random.Range(7, 13); //TODO: Endit these values based on stage size?
        switch (currentClrCon)
        {
            case ClearCondition.Survive:
                //SET TIME 
                //TODO: THESE VALUES WILL NEED TO BE TESTED AND CHANGED LATER
                timeToSurvive = Random.Range(20, 40);
                //Update UI to say survive
                targetEnemyCount = randomEnemyCount / 2;
                
                break;
            case ClearCondition.Eliminate:
                //Update UI to say kill all
                targetEnemyCount = randomEnemyCount;
                break;
        }

        spawner.enemiesToSpawn = targetEnemyCount;
    }

    private void EndStage()
    {
        isStageCleared = true;
        //Spawn Buff
        //Open door to next stage
        //TODO: Update Game manager stage count
    }
    private void Awake()
    {
        isStageCleared = false;
        currentClrCon = ClearCondition.Survive;
        enemiesAlive = 0;
        timeToSurvive = 0;
        targetEnemyCount = 0;
        
        spawner = FindAnyObjectByType<EnemySpawner>();


        //THIS IS TEMPORARY
        //TODO: FIX THIS TO ACTUALLY WORK PROPERLY, THIS IS FOR TESTING
        BeginStage();
    }
    void Update()
    {
        if (!isStageCleared)
        {
            if (currentClrCon == ClearCondition.Survive)
            {
                timeToSurvive -= Time.deltaTime;
                CheckClearCondition();
                if (enemiesAlive < targetEnemyCount)
                    spawner.enabled = true;
                    spawner.enemiesToSpawn = targetEnemyCount - enemiesAlive;
            }
        }
    }
}
