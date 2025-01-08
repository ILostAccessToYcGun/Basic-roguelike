using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public float attackDelay;
    public Quaternion startAngle;
    public Quaternion midAngle;
    public Quaternion endAngle;
    public float swingSpeed;

    //attackDelay is in seconds, when the player clicks to attack, the delay will go up a certain amount,
    //based on cooldown and will count down each frame

    /// <summary>
    /// Performs a sword swing
    /// </summary>
    public void SwordAttack()
    {
        //if (swordSwingFrame >= (500 - f_CD) * 2)
        //{
        //    isSwingingSword = false; //exit condition
        //    swordSwingFrame = 0;
        //    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    PointWeapon(WPN, mousePos);
        //    currentAngle.eulerAngles -= new Vector3(0, 0, swordSwingRange * attackDirection);
        //    //attackTimer = (3 - (f_CD/100) > 0) ? 3 - (f_CD / 100) : 0.01f; //getting rid of the in between attack cd because it feels really bad on sword, this will need to exist on projectile weapons
        //    attackTimer = 0.01f;
        //}
        //else
        //{
        //    currentAngle.eulerAngles += new Vector3(0, 0, (swordSwingRange / (500 - f_CD)) * attackDirection);
        //    swordSwingFrame++;
        //}
        //WPN.transform.rotation = currentAngle;

        //CD/Attack speed rework
        if (swordSwingFrame >= (500 - f_CD) * 2)
        {
            isSwingingSword = false; //exit condition
            swordSwingFrame = 0;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PointWeapon(WPN, mousePos);
            currentAngle.eulerAngles -= new Vector3(0, 0, swordSwingRange * attackDirection);
            //attackTimer = (3 - (f_CD/100) > 0) ? 3 - (f_CD / 100) : 0.01f; //getting rid of the in between attack cd because it feels really bad on sword, this will need to exist on projectile weapons
            attackTimer = 0.01f;
        }
        else
        {
            currentAngle.eulerAngles += new Vector3(0, 0, (swordSwingRange / (500 - f_CD)) * attackDirection);
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
        CD = 0;
        JUMP = 1;
        jHeight = 2;
        WGHT = 5;


        InitializeStats();

        uiManager = FindAnyObjectByType<UIManager>();

        ITEM = Weapons.Sword;
        swordObject = GetComponentInChildren<Sword>();

        isSwingingSword = false;
        swordSwingRange = 45;
        //swordSwingIncrement = ;
    }

    private void Start()
    {
        uiManager.UpdatePlayerStatsUI(); //grrrrrrrrrr
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
            {
                
                if (attackDelay < 0) //if we have no more delay, stop the attack
                {
                    WPN.transform.rotation = midAngle;
                    isSwingingSword = false;
                }
                else
                {
                    //float distanceZ = Mathf.Abs(endAngle.z) - Mathf.Abs(currentAngle.z);
                    //float distanceW = Mathf.Abs(endAngle.w) - Mathf.Abs(currentAngle.w);
                    //OKAY IM REALLY COSE TO BEING HAPPY HERE. THERE IS A BUG WHERE IT GOES ALL THE WAY FACING RIGHT, BUT ONLY HALF FACING LEFT
                    float distanceZ = endAngle.z - currentAngle.z;
                    float distanceW = endAngle.w - currentAngle.w;
                    currentAngle = new Quaternion(0, 0, Mathf.Lerp(currentAngle.z, endAngle.z, Time.deltaTime / (distanceZ / swingSpeed)), Mathf.Lerp(currentAngle.w, endAngle.w, Time.deltaTime / (distanceW / swingSpeed)));
                    WPN.transform.rotation = currentAngle;
                    attackDelay -= Time.deltaTime;
                }
                //SwordAttack();
            }
            else
                PointWeapon(WPN, mousePos);

            ////Attacking TODO: Optimize this later with virtual methods and runtime polymorphism
            //if (attackTimer > 0)
            //    attackTimer -= Time.deltaTime;
            //else
            //{
            //    if (Input.GetAxisRaw("Attack") == 1 && !isSwingingSword)
            //    {
            //        isSwingingSword = true;
            //        swordSwingFrame = 0;

            //        if (mousePos.x > transform.position.x)
            //            attackDirection = -1;
            //        else
            //            attackDirection = 1;

            //        currentAngle.eulerAngles = WPN.transform.rotation.eulerAngles;
            //        currentAngle.eulerAngles -= new Vector3(0, 0, swordSwingRange * attackDirection);
            //    }
            //}

            //CD/Attackspeed rework
            //when the player clicks to attack, the delay will go up a certain amount,
            //based on cooldown and will count down each frame

            if (Input.GetAxisRaw("Attack") == 1 && !isSwingingSword) //if we click and we arent already swinging
            {
                attackDelay = 2f / (f_CD + 100/100f); //i think thats a not bad algorithm,
                //0.5 attacks persecond by default. this can and probably will change
                isSwingingSword = true;

                //get direction
                if (mousePos.x > transform.position.x)
                    attackDirection = -1;
                else
                    attackDirection = 1;


                //now we need to wind up the swing, offset the sword angle by the swing range
                currentAngle.eulerAngles = WPN.transform.rotation.eulerAngles;
                startAngle.eulerAngles = currentAngle.eulerAngles - new Vector3(0, 0, swordSwingRange * attackDirection); //start angle
                midAngle = currentAngle; //mid angle
                endAngle.eulerAngles = currentAngle.eulerAngles + new Vector3(0, 0, swordSwingRange * attackDirection); //end angle
                currentAngle = startAngle;

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
