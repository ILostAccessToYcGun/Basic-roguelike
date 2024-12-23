using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NormalEnemy : Enemies
{
    //TODO: The way im currently doing AI, is by doing speed and time, I think a smarter way of doing this is by picking a random location on the map
    //but thats for a later TODO
    
    
    public enum Actions { MoveRight, MoveLeft, Jump, Wait }
    public List<Actions> actionList;
    public int currentAction;

    private float MoveRightDuration; //how long we are doing the action
    private float MoveLeftDuration; //how long we are doing the action
    private float JumpDuration; //how long we are doing the action
    private float WaitDuration; //how long we are doing the action
    private float actionCooldown; //how long between actions are made, counter begins when the action starts

    public void NormalAI()
    {
        switch (currentAI)
        {
            case AI.Roaming:
                if (actionCooldown <= 0)
                {
                    //randomly choose an action
                    currentAction = Random.Range(0, 4);

                    //randomly choose the duration for the action
                    switch (currentAction)
                    {
                        case 0:// Actions.MoveRight
                            actionList.Add(Actions.MoveRight);
                            MoveRightDuration = Random.Range(0.5f, 4f);
                            break;
                        case 1:// Actions.MoveLeft
                            actionList.Add(Actions.MoveLeft);
                            MoveLeftDuration = Random.Range(0.5f, 4f);
                            break;
                        case 2:// Actions.Jump
                            actionList.Add(Actions.Jump);
                            JumpDuration = Random.Range(0.5f, 4f);
                            break;
                        case 3:// Actions.Wait
                            actionList.Add(Actions.Wait);
                            WaitDuration = Random.Range(0.5f, 4f);
                            break;
                    }
                    //randomly choose the cooldown between each action
                    actionCooldown = Random.Range(1f, 5f);
                }
                else
                    actionCooldown -= Time.deltaTime;


                if (!isWallJumping)
                {
                    if (MoveRightDuration > 0)
                    {
                        MoveRight();
                        MoveRightDuration -= Time.deltaTime;
                    }
                    else
                        actionList.Remove(Actions.MoveRight);

                    if (MoveLeftDuration > 0)
                    {
                        MoveLeft();
                        MoveLeftDuration -= Time.deltaTime;
                    }
                    else
                        actionList.Remove(Actions.MoveLeft);

                    if (JumpDuration > 0)
                    {
                        Jump();
                        JumpDuration -= Time.deltaTime;
                    }
                    else
                        actionList.Remove(Actions.Jump);

                    if (WaitDuration > 0)
                        WaitDuration -= Time.deltaTime; //do nothing
                    else
                        actionList.Remove(Actions.Wait);
                }
                else
                {
                    switch (wallJumpDir)
                    {
                        case wallJumpDirection.Left:
                            MoveLeft();
                            break;
                        case wallJumpDirection.Right:
                            MoveRight();
                            break;
                    }

                    if (wallJumpDuration <= 0)
                    {
                        isWallJumping = false;
                        f_SPD -= SPD / 2;
                    }

                    else
                        wallJumpDuration -= Time.deltaTime;
                }
                break;

            case AI.Aggro:
                if (!isWallJumping)
                {
                    //Left or Right
                    if (transform.position.x < player.transform.position.x)
                        MoveRight();
                    else
                        MoveLeft();

                    if (transform.position.y < player.transform.position.y - (0.5f * player.f_SIZE))
                        Jump();
                }
                else
                {
                    switch (wallJumpDir)
                    {
                        case wallJumpDirection.Left:
                            MoveLeft();
                            break;
                        case wallJumpDirection.Right:
                            MoveRight();
                            break;
                    }

                    if (wallJumpDuration <= 0)
                    {
                        isWallJumping = false;
                        f_SPD -= SPD / 2;
                    }

                    else
                        wallJumpDuration -= Time.deltaTime;
                }    
                break;
        }
    }

    /// <summary>
    /// Initialises the stats for the Normal Enemy
    /// </summary>
    public void Awake()
    {
        //main stats
        MaxHP = 20;
        ATK = 3;
        SPD = 3;
        DEF = 5;

        //special stats
        CD = 0;
        JUMP = 1;
        SIZE = 1f;
        WGHT = 3;

        InitializeStats();

        ITEM = Weapons.None;
        currentAI = AI.Roaming;

        player = FindObjectOfType<Characters>();
        stageManager = FindAnyObjectByType<StageManager>();
        stageManager.UpdateEnemyCount(1);

        visionLayerMasks = LayerMask.GetMask("Player", "Ground", "Wall"); //I feel like I should generalize this here

        visionRange = 10f;

        //isSwingingSword = false;
        //swordSwingRange = 45;
        //swordSwingIncrement = swordSwingRange / (500 - f_CD);
        //no sword just yet
    }
    void Update()
    {
        if (isActiveAndEnabled)
        {
            DeathCheck();
            LOStoPlayer();
            NormalAI();
            Gravity();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            player.CurrentHP -= f_ATK;
    }
}
