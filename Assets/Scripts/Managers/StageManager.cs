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

    public List<GameObject> stages;
    public GameObject stageToSpawn;
    public GameObject stagePOI;
    public List<Vector2> allStagePOIs;
    public int stagePOIIndex;

    public GameObject shop;
    public bool isNextStageShop = false;

    public Door[] stageDoors;
    public PointOfReference pointOfReference;

    //public float stageSize;
    public enum ClearCondition { Intermission, Survive, Eliminate };
    public ClearCondition currentClrCon;

    public void UpdateEnemyCount(int update)
    {
        enemiesAlive += update;
        uiManager.EliminateEnemies_UpdateUIEnemyCounter();
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
        isStageCleared = false;
        int random = Random.Range(1, 3);
        currentClrCon = (ClearCondition)random;

        int randomEnemyCount = Random.Range(7, 13); //TODO: Endit these values based on stage size?
        switch (currentClrCon)
        {
            case ClearCondition.Survive:
                //SET TIME 
                //TODO: THESE VALUES WILL NEED TO BE TESTED AND CHANGED LATER
                timeToSurvive = Random.Range(20, 40);
                uiManager.Survive_Toggle(true);
                targetEnemyCount = randomEnemyCount / 2;
                break;
            case ClearCondition.Eliminate:
                uiManager.EliminateEnemies_Toggle(true);
                targetEnemyCount = randomEnemyCount;
                break;
        }
        spawner.enemiesToSpawn = targetEnemyCount;
    }

    private void EndStage()
    {
        isStageCleared = true;
        currentClrCon = ClearCondition.Intermission;
        uiManager.StageClear_StageCleared();
        spawner.enabled = false;
        foreach (Door door in stageDoors)
        {
            if (door.doorType == Door.DoorType.Exit)
                door.OpenDoor();
        }
        //spawn the shop buffs and crystal stuff
        SpawnStage();
        buffManager.SpawnBuffsInShop();
        gameManager.IncrementStageCount();
        SpawnStage();

    }

    private void MoveToPointOfReference()
    {
        transform.position = pointOfReference.gameObject.transform.position;
    }

    public void NextStagePOI() //eventually I want to lerp this so its smooth?
    {
        if (stagePOIIndex < allStagePOIs.Count - 1)
            stagePOIIndex++;
        stagePOI.transform.position = allStagePOIs[stagePOIIndex];
    }
    public void PreviousStagePOI() //eventually I want to lerp this so its smooth?
    {
        if (stagePOIIndex > 0)
            stagePOIIndex--;
        stagePOI.transform.position = allStagePOIs[stagePOIIndex];
    }

    private void SpawnStage()
    {
        stageDoors = new Door[2];
        switch (isNextStageShop)
        {
            case false:
                int rand = Random.Range(0, stages.Count);
                stageToSpawn = stages[rand];
                break;
            case true:
                stageToSpawn = shop;
                break;
        }
        
        GameObject stageInstance;
        stageInstance = Instantiate(stageToSpawn, transform.position, transform.rotation);
        pointOfReference = stageInstance.GetComponentInChildren<PointOfReference>();

        stageDoors = stageInstance.GetComponentsInChildren<Door>();
        foreach (Door door in stageDoors)
        {
            door.SetDoorStats();
            if (door.doorType == Door.DoorType.Entry || door.doorType == Door.DoorType.Pass)
            {
                stageInstance.transform.position = new Vector2(this.transform.position.x + 21f, this.transform.position.y + (door.transform.localPosition.y * -1));
            }
        }

        if (isNextStageShop)
        {
            buffManager.buffLocations = stageInstance.GetComponentsInChildren<BuffSpawnLocation>();
            Debug.Log(stageInstance.GetComponentsInChildren<BuffSpawnLocation>());
            buffManager.ResetBuffIndex();
        }
        isNextStageShop = !isNextStageShop;

        allStagePOIs.Add(stageInstance.transform.position);
        spawner.ChangeCenter(stageInstance.transform.position);
        spawner.MoveSpawner();
        MoveToPointOfReference();
    }

    public void StageBeginSequence()
    {
        foreach (Door door in stageDoors)
        {
            door.CloseDoor();
        }
        spawner.enabled = true;
        BeginStage();
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
        gameManager.SetStageManager(this);


        allStagePOIs.Add(new Vector2(0, 0));
        pointOfReference = FindAnyObjectByType<PointOfReference>();
        MoveToPointOfReference();
        SpawnStage();

        //BeginStage();


    }
    void Update()
    {
        if (!isStageCleared)
        {
            if (currentClrCon == ClearCondition.Survive)
            {
                timeToSurvive -= Time.deltaTime;
                uiManager.Survive_UpdateUISurviveTimer();
                CheckClearCondition();
                if (enemiesAlive < targetEnemyCount)
                {
                    spawner.enabled = true;
                    spawner.enemiesToSpawn = targetEnemyCount - enemiesAlive;
                }
            }
        }
    }
}
