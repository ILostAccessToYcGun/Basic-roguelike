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
    public bool isSecondSwing;
    private int swordSwingFrame = 0;
    private int singularFrameDelay = 1;

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
                    singularFrameDelay = 1;
                }
                else
                {
                    //SPEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEED
                    //how the frogs did that kinda work
                    if (distanceZ <= (halfDistanceZ - 0.05f)/4)
                        swingSpeedMultiplierZ = distanceZ / halfDistanceZ;
                    else
                        swingSpeedMultiplierZ = (halfDistanceZ - 0.05f) / halfDistanceZ;

                    if (distanceW <= (halfDistanceW - 0.05f)/4)
                        swingSpeedMultiplierW = distanceW / halfDistanceW;
                    else
                        swingSpeedMultiplierW = (halfDistanceW - 0.05f) / halfDistanceW;

                    currentAngle = new Quaternion(0, 0, Mathf.Lerp(currentAngle.z, endAngle.z, Time.deltaTime / (distanceZ / (swingSpeed * (1 + f_CD)) * (1 - swingSpeedMultiplierZ))), Mathf.Lerp(currentAngle.w, endAngle.w, Time.deltaTime / (distanceW / (swingSpeed * (1 + f_CD)) * (1 - swingSpeedMultiplierW))));
                    WPN.transform.rotation = currentAngle;
                    attackDelay -= Time.deltaTime;

                    distanceZ = Mathf.Abs(endAngle.z - currentAngle.z);
                    distanceW = Mathf.Abs(endAngle.w - currentAngle.w);
                }
                //SwordAttack();
            }
            else
            {
                if (singularFrameDelay > 0)
                {
                    PointWeapon(WPN, (Vector2)mousePos);
                }
                else
                {
                    GetPointAngle(WPN, (Vector2)mousePos);
                }
            }

            //CD/Attackspeed rework
            //when the player clicks to attack, the delay will go up a certain amount,
            //based on cooldown and will count down each frame

            if (Input.GetAxisRaw("Attack") == 1) //if we click and we arent already swinging
            {
                if (!isSwingingSword && singularFrameDelay <= 0)
                {
                    attackDelay = 2f / (f_CD + 100 / 100f); //i think thats a not bad algorithm, 0.5 attacks persecond by default. this can and probably will change
                    //^^ this should be the only algorithm needed for ranged weapons, for melee weapons they need their attack animation sped up aswell

                    isSwingingSword = true;

                    //get direction
                    if (mousePos.x > transform.position.x)
                        attackDirection = -1;
                    else
                        attackDirection = 1;

                    currentAngle.eulerAngles = pointAngle.eulerAngles;

                    if (isSecondSwing)
                    {
                        attackDirection *= -1;
                        isSecondSwing = false;
                    }
                    else
                        isSecondSwing = true;


                    //now we need to wind up the swing, offset the sword angle by the swing range
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

                    //if (!isSecondSwing)
                    //{
                        
                        
                    //}
                    //else
                    //{
                    //    //now we need to wind up the swing, offset the sword angle by the swing range
                    //    startAngle.eulerAngles = currentAngle.eulerAngles - new Vector3(0, 0, swordSwingRange * attackDirection * -1); //start angle
                    //    midAngle = currentAngle; //mid angle
                    //    endAngle.eulerAngles = currentAngle.eulerAngles + new Vector3(0, 0, swordSwingRange * attackDirection * -1); //end angle
                    //    currentAngle = startAngle;

                    //    distanceZ = Mathf.Abs(endAngle.z - currentAngle.z);
                    //    distanceW = Mathf.Abs(endAngle.w - currentAngle.w);

                    //    fullDistanceZ = distanceZ;
                    //    fullDistanceW = distanceW;

                    //    halfDistanceZ = distanceZ / 2;
                    //    halfDistanceW = distanceW / 2;
                        
                    //}
                }
                else
                {
                    if (singularFrameDelay > 0)
                        singularFrameDelay--;
                }
                
                
            }
            else
            {
                //stop continuous swing
                if (isSecondSwing)
                    isSecondSwing = false;

                
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
