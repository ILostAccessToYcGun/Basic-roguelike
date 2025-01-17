using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int enemiesAlive;
    public float timeToSurvive;
    public bool isStageCleared;

    public int targetEnemyCount;
    private BuffManager buffManager;
    private EnemySpawner spawner;
    private UIManager uiManager;
    private GameManager gameManager;

    public List<Rigidbody2D> stages;
    public Rigidbody2D stageToSpawn; //TODO: For later
    public GameObject stagePOI;

    public List<Door> doors;

    //public float stageSize;
    public enum ClearCondition { Intermission, Survive, Eliminate };
    public ClearCondition currentClrCon;

    public void UpdateEnemyCount(int update)
    {
        enemiesAlive += update;
        uiManager.UpdateUIEnemyCounter();
        CheckClearCondition();
    }

    private void CheckClearCondition()
    {
        if (!isStageCleared)
        {
            switch (currentClrCon)
            {
                case ClearCondition.Survive:
                    if (timeToSurvive < 0)
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
                uiManager.SurviveUIToggle(true);
                targetEnemyCount = randomEnemyCount / 2;
                break;
            case ClearCondition.Eliminate:
                uiManager.EliminateEnemiesUIToggle(true);
                targetEnemyCount = randomEnemyCount;
                break;
        }
        spawner.enemiesToSpawn = targetEnemyCount;
    }

    private void EndStage()
    {
        isStageCleared = true;
        uiManager.StageCleared();
        //Open door to shop
        //spawn the shop buffs and crystal stuff
        buffManager.SpawnBuff();
        gameManager.IncrementStageCount();
    }

    private void AdvanceStagePosition() //Find stage point of reference and warp
    {
        //transform.position = new Vector2(transform.position.x + 43, transform.position.y);
    }

    private void MoveStagePOI(Vector3 newPosition) //eventually I want to lerp this so its smooth?
    {
        stagePOI.transform.position = newPosition;
    }

    private void SpawnStage()
    {
        int rand = Random.Range(0, stages.Count);
        stageToSpawn = stages[rand];
        Rigidbody2D stageInstance;

        //point of reference system
        //in short i dont wanna hard code this. I think what i should do is
        //make a point of reference empty object that the stage manager will teleport to when spaning the stage.
        //the point of reference will be in the hallway section of the map, so we have one elevation correct, then we use the
        //written code to move the stage to be spawned to the reference point elevation
        

        stageInstance = Instantiate(stageToSpawn, transform.position, transform.rotation);

        Door[] stageDoors = stageInstance.GetComponentsInChildren<Door>();

        foreach (Door door in stageDoors)
        {
            if (door.doorType == Door.DoorType.Entry)
            {
                stageInstance.transform.position = new Vector2(stageInstance.transform.position.x, door.transform.localPosition.y * -1);
            }
        }
    }

    private void Awake()
    {
        isStageCleared = false;
        currentClrCon = ClearCondition.Intermission;
        enemiesAlive = 0;
        timeToSurvive = 0;
        targetEnemyCount = 0;
        
        spawner = FindAnyObjectByType<EnemySpawner>();
        buffManager = FindAnyObjectByType<BuffManager>();
        uiManager = FindAnyObjectByType<UIManager>();
        gameManager = FindAnyObjectByType<GameManager>();

        //BeginStage();
    }
    void Update()
    {
        if (!isStageCleared)
        {
            if (currentClrCon == ClearCondition.Survive)
            {
                timeToSurvive -= Time.deltaTime;
                uiManager.UpdateUISurviveTimer();
                CheckClearCondition();
                if (enemiesAlive < targetEnemyCount)
                    spawner.enabled = true;
                    spawner.enemiesToSpawn = targetEnemyCount - enemiesAlive;
            }
        }
    }
}
