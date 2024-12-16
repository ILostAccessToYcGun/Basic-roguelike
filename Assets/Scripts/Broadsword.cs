using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Broadsword : Characters
{
    //okay, the unique things about the broadsword class is what weapon it holds, and how it attacks.
    //im pretty sure im wrong in saying that, cuz enemies might be able to get swords later but for the sake of tempo we keep coding

    //some of these things will want to be moved to a character class, like the mouse position

    //OKAY, im pretty happy with how this is, it is now time to remake the character class



    //updating the data values because we are the sword class

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
        SIZE = 2f;
        WGHT = 5;


        InitializeStats();

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
            Movement();

            //if we are swinging, stop pointing and swing
            if (isSwingingSword)
                SwordAttack();
            else
                PointWeapon(WPN, mousePos);

            //Attacking
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Recover();
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = false;
    }


}
