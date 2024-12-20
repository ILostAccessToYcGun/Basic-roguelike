using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Enemies
{
    //TODO: change the access modifiers on these once ur done
    public float temptimer;
    public int enemiesToSpawn; //public for now, but will be dictated by the stage manager

    public Rigidbody2D normalEnemy;



    //Enemy Spawner
    //  Make an enemy spawner object with an isTrigger collider.
    //  https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Object.Instantiate.html
    //{ LOOP }
    //  when called to spawn enemies:
    //      
    //      
    //      
    //      AKA if we are in an empty spot and we are above ground, spawn an enemy
    //      
    //{ END LOOP } 

    private void Awake()
    {
        //These stats are the additional stats given to enemies based on what buffs the player gets.
        //E.g +1 player damage, +1 enemy speed would make f_SPD = 1;
        f_MaxHP = 0;
        f_ATK = 0;
        f_SPD = 0;
        f_DEF = 0;

        f_CD = 0;
        f_JUMP = 0;
        f_SIZE = 0;
        f_WGHT = 0;

        visionLayerMasks = LayerMask.GetMask("Ground");
    }

    //TODO: Eventually when I add juice to this game, I want the spawner to move inbetween the random locations, instead of teleporting
    public void MoveSpawner()
    {
        //TODO: Choose which enemy to spawn (FUTUTRE)
        //chose a random location in the scene
        //get the stage size first, im going to hard code it for now because I havent gotten to that buff
        //x between -19 and 19
        //y between -9.5 and 9
        transform.position = new Vector2(Random.Range(-19f, 19), Random.Range(-9.5f, 9.5f));
    }

    public void CheckValidSpawnLocation()
    {
        //RayCast a line down, if we don't hit the ground, choose a different spot
        hit = Physics2D.Raycast(transform.position, transform.up * -1, 30f, visionLayerMasks);
        Debug.DrawRay(transform.position, transform.up * -1 * 30f, Color.red);
        if (!hit)
            MoveSpawner();
    }

    public void SpawnEnemy()
    {
        //spawn an enemy
        Rigidbody2D enemy;
        enemy = Instantiate(normalEnemy, transform.position, transform.rotation);
        NormalEnemy enemyScript = enemy.GetComponent<NormalEnemy>();

        //updating the spawned enemy stats to match the current buffs
        enemyScript.f_MaxHP += this.f_MaxHP;
        enemyScript.f_ATK += this.f_ATK;
        enemyScript.f_SPD += this.f_SPD;
        enemyScript.f_DEF += this.f_DEF;

        enemyScript.f_CD += this.f_CD;
        enemyScript.f_JUMP += this.f_JUMP;
        enemyScript.f_SIZE += this.f_SIZE;
        enemyScript.f_WGHT += this.f_WGHT;

        //decrement number of enemies to spawn
        enemiesToSpawn--;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MoveSpawner();
    }

    private void Update()
    {
        if (enemiesToSpawn > 0)
        {
            CheckValidSpawnLocation();
            if (temptimer <= 0)
            {
                SpawnEnemy();
                temptimer = 0.2f;
            }
            else
            {
                temptimer -=Time.deltaTime;
            }
            
        }
        else
        {
            this.enabled = false;
        }
    }
}
