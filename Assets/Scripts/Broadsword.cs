using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Broadsword : Characters
{
    private float swordSwingRange;
    private float swordSwingIncrement;
    public bool isSwingingSword;
    private int swordSwingFrame = 0;

    private Sword swordObject;

    /// <summary>
    /// Performs a sword swing
    /// </summary>
    public void SwordAttack()
    {
        if (swordSwingFrame >= (500 - f_CD) * 2)
        {
            isSwingingSword = false; //exit condition
            swordSwingFrame = 0;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PointWeapon(WPN, mousePos);
            currentAngle.eulerAngles -= new Vector3(0, 0, swordSwingRange * attackDirection);
            attackTimer = (3 - (f_CD/100) > 0) ? 3 - (f_CD / 100) : 0.01f;
        }
        else
        {
            currentAngle.eulerAngles += new Vector3(0, 0, swordSwingIncrement * attackDirection);
            swordSwingFrame++;
        }
        WPN.transform.rotation = currentAngle;
    }

    /// <summary>
    /// Initialises the stats for the Broadsword Class
    /// </summary>
    public void Awake()
    {
        //main stats
        MaxHP = 60;
        ATK = 5;
        SPD = 5;
        DEF = 10;

        //special stats
        CD = 300;
        JUMP = 1;
        jHeight = 2;
        WGHT = 5;


        InitializeStats();

        uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdatePlayerStatsUI();

        ITEM = Weapons.Sword;
        swordObject = GetComponentInChildren<Sword>();

        isSwingingSword = false;
        swordSwingRange = 45;
        swordSwingIncrement = swordSwingRange/(500 - f_CD);
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            DeathCheck();
            Movement();
            OtherControls();

            //if we are swinging, stop pointing and swing
            if (isSwingingSword)
                SwordAttack();
            else
                PointWeapon(WPN, mousePos);

            //Attacking TODO: Optimize this later with virtual methods and runtime polymorphism
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;
            else
            {
                if (Input.GetAxisRaw("Attack") == 1 && !isSwingingSword)
                {
                    isSwingingSword = true;
                    swordSwingFrame = 0;

                    if (mousePos.x > transform.position.x)
                        attackDirection = -1;
                    else
                        attackDirection = 1;

                    currentAngle.eulerAngles = WPN.transform.rotation.eulerAngles;
                    currentAngle.eulerAngles -= new Vector3(0, 0, swordSwingRange * attackDirection);
                }
            }
        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
        
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    
    //}
}
