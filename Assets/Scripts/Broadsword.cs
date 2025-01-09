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
    [SerializeField] float swingSpeedMultiplierZ;
    [SerializeField] float swingSpeedMultiplierW;

    public float distanceZ;
    public float distanceW;
    public float halfDistanceZ;
    public float halfDistanceW;
    public float fullDistanceZ;
    public float fullDistanceW;

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

                    //as the distance approaches half the initial distance, increase the speed,
                    //as the distance moves away from half the initial different, decrease the speed

                    
                    //SPEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEED
                    if (distanceZ <= halfDistanceZ)
                    {
                        swingSpeedMultiplierZ = distanceZ / halfDistanceZ;
                    }
                    else // distanceZ > halfDistanceZ
                    {
                        //this speed should be fairly fast here

                        //swingSpeedMultiplierZ = distanceZ / halfDistanceZ;
                        //swingSpeedMultiplierZ = 0;
                        //swingSpeedMultiplierZ = ((fullDistanceZ - distanceZ <= 0) ? 0.05f : fullDistanceZ - distanceZ) / halfDistanceZ;
                    }

                    if (distanceW <= halfDistanceW)
                    {
                        swingSpeedMultiplierW = distanceW / halfDistanceW;
                    }
                    else // distanceW > halfDistanceW
                    {
                        //this speed should be fairly fast here

                        //swingSpeedMultiplierW = distanceW / halfDistanceW;
                        //swingSpeedMultiplierW = 0;
                        //swingSpeedMultiplierW = ((fullDistanceW - distanceW <= 0) ? 0.05f : fullDistanceW - distanceW) / halfDistanceW;
                    }

                    currentAngle = new Quaternion(0, 0, Mathf.Lerp(currentAngle.z, endAngle.z, Time.deltaTime / (distanceZ / swingSpeed * (1 - swingSpeedMultiplierZ))), Mathf.Lerp(currentAngle.w, endAngle.w, Time.deltaTime / (distanceW / swingSpeed * (1 - swingSpeedMultiplierW))));
                    WPN.transform.rotation = currentAngle;
                    attackDelay -= Time.deltaTime;

                    distanceZ = Mathf.Abs(endAngle.z - currentAngle.z);
                    distanceW = Mathf.Abs(endAngle.w - currentAngle.w);
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

                distanceZ = Mathf.Abs(endAngle.z - currentAngle.z);
                distanceW = Mathf.Abs(endAngle.w - currentAngle.w);

                fullDistanceZ = distanceZ;
                fullDistanceW = distanceW;

                halfDistanceZ = distanceZ / 2;
                halfDistanceW = distanceW / 2;

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
