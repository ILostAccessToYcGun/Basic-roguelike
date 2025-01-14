using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Broadsword : Characters
{
    private float swordSwingRange;
    public bool isSwingingSword;
    private bool isSecondSwing;
    private int singularFrameDelay = 1;

    private Sword swordObject;

    [SerializeField] float attackDelay;
    private Quaternion startAngle;
    private Quaternion midAngle;
    private Quaternion endAngle;
    [SerializeField] float swingSpeed;
    [SerializeField] float swingSpeedMultiplierZ;
    [SerializeField] float swingSpeedMultiplierW;

    private float distanceZ;
    private float distanceW;
    private float halfDistanceZ;
    private float halfDistanceW;
    private float fullDistanceZ;
    private float fullDistanceW;

    //attackDelay is in seconds, when the player clicks to attack, the delay will go up a certain amount,
    //based on cooldown and will count down each frame


    private void PrepareSwordSwing()
    {
        if (!isSwingingSword && singularFrameDelay <= 0)
        {
            attackDelay = 2f / (f_ATKSPD + 100 / 100f); 
            //i think thats a not bad algorithm, 0.5 attacks persecond by default. this can and probably will change
            //^^ this should be the only algorithm needed for ranged weapons, for melee weapons they need their attack animation sped up aswell
            //TODO: decide if you want f_CD to scale the speed slower, right now, 20CD is pretty fast

            //Eventually what I want to do is change the attack once it gets fast enough, from a moving trigger collider to a hitscan, to make processing easier and it won't be noticeable

            isSwingingSword = true;

            if (mousePos.x > transform.position.x)
                attackDirection = -1;
            else
                attackDirection = 1;

            if (isSecondSwing)
            {
                attackDirection *= -1;
                isSecondSwing = false;
            }
            else
                isSecondSwing = true;

            currentAngle = pointAngle;
            startAngle.eulerAngles = currentAngle.eulerAngles - new Vector3(0, 0, swordSwingRange * attackDirection);
            midAngle = currentAngle;
            endAngle.eulerAngles = currentAngle.eulerAngles + new Vector3(0, 0, swordSwingRange * attackDirection);
            currentAngle = startAngle;

            distanceZ = Mathf.Abs(endAngle.z - currentAngle.z);
            distanceW = Mathf.Abs(endAngle.w - currentAngle.w);
            fullDistanceZ = distanceZ;
            fullDistanceW = distanceW;
            halfDistanceZ = distanceZ / 2;
            halfDistanceW = distanceW / 2;
        }
        else
        {
            if (singularFrameDelay > 0)
                singularFrameDelay--;
        }
    }

    public void SwordAttack()
    {
        if (attackDelay < 0) //if we have no more delay, stop the attack
        {
            WPN.transform.rotation = midAngle;
            isSwingingSword = false;
            singularFrameDelay = 1;

            swordObject.ResetTags();
        }
        else
        {
            if (distanceZ <= (halfDistanceZ - 0.05f) / 4)
                swingSpeedMultiplierZ = distanceZ / halfDistanceZ;
            else
                swingSpeedMultiplierZ = (halfDistanceZ - 0.05f) / halfDistanceZ;

            if (distanceW <= (halfDistanceW - 0.05f) / 4)
                swingSpeedMultiplierW = distanceW / halfDistanceW;
            else
                swingSpeedMultiplierW = (halfDistanceW - 0.05f) / halfDistanceW;

            currentAngle = new Quaternion(0, 0, Mathf.Lerp(currentAngle.z, endAngle.z, Time.deltaTime / (distanceZ / (swingSpeed * (1 + f_ATKSPD)) * (1 - swingSpeedMultiplierZ))), Mathf.Lerp(currentAngle.w, endAngle.w, Time.deltaTime / (distanceW / (swingSpeed * (1 + f_ATKSPD)) * (1 - swingSpeedMultiplierW))));
            WPN.transform.rotation = currentAngle;
            attackDelay -= Time.deltaTime;

            distanceZ = Mathf.Abs(endAngle.z - currentAngle.z);
            distanceW = Mathf.Abs(endAngle.w - currentAngle.w);
        }
    }

    public void Awake()
    {
        //main stats
        MaxHP = 60;
        ATK = 5;
        SPD = 5;
        DEF = 10;

        //special stats
        ATKSPD = 0;
        JUMP = 1;
        REG = 5;
        WGHT = 5;

        InitializeStats();

        ITEM = Weapons.Sword;
        swordObject = GetComponentInChildren<Sword>();

        isSwingingSword = false;
        swordSwingRange = 45;
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
            RegenHealth();

            //if we are swinging, stop pointing and swing
            if (isSwingingSword)
                SwordAttack();
            else
            {
                if (singularFrameDelay > 0)
                    PointWeapon(WPN, (Vector2)mousePos);
                else
                    GetPointAngle(WPN, (Vector2)mousePos);
            }

            if (Input.GetAxisRaw("Attack") == 1) 
                PrepareSwordSwing();
            else
            {
                //if (isSecondSwing)
                //    isSecondSwing = false;

                //stop continuous swing
                isSecondSwing = false;
            }
        }
    }
}
