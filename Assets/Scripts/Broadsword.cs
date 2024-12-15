using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Broadsword : Units
{
    //okay, the unique things about the broadsword class is what weapon it holds, and how it attacks.
    //im pretty sure im wrong in saying that, cuz enemies might be able to get swords later but for the sake of tempo we keep coding

    //some of these things will want to be moved to a character class, like the mouse position
    private Vector3 mousePos; 

    //updating the data values because we are the sword class

    public float swingRange;
    public bool isSwinging;
    public Quaternion atkAngle;
    private int atkDir;

    public Quaternion currentAngle;
    Vector3 currentEulerAngles;

    Quaternion currentRotation;



    //for the sword attack we need to interrupt the weapon pointing animation, so we need something for that
    public void SwordAttack()
    {
        //the last parameter is how fast it is
        // i wanna lerp the speed so its fastest when the blade passses through the middle

        

        //apply the Quaternion.eulerAngles change to the gameObject
        WPN.transform.rotation = currentAngle;

        Quaternion endAngle = Quaternion.identity;
        Quaternion startAngle = Quaternion.identity;
        switch (atkDir)
        {
            case -1:
                endAngle.eulerAngles = atkAngle.eulerAngles - new Vector3(0, 0, swingRange);
                //if (endAngle < -180)
                //{
                //    endAngle = 360 + (endAngle);
                //}
                
                startAngle.eulerAngles = atkAngle.eulerAngles + new Vector3(0, 0, swingRange);

                if (currentAngle.eulerAngles.z < endAngle.eulerAngles.z) //PROBLEM HERE--------------------------------------------
                {
                    isSwinging = false; //exit condition
                    Debug.Log("Hmm");
                }
                    

                //Debug.Log("atkAngle.eulerAngles.z: " + (atkAngle.eulerAngles.z));
                //Debug.Log("endAngle.eulerAngles.z: " + (endAngle.eulerAngles.z));
                //Debug.Log("startAngle.eulerAngles.z: " + (startAngle.eulerAngles.z));
                //Debug.Log("startAngle" + (startAngle));
                //Debug.Log("WPN.transform.localEulerAngles.z" + (WPN.transform.localEulerAngles.z));


                break;
            case 1:

                endAngle.eulerAngles = currentAngle.eulerAngles + new Vector3(0, 0, swingRange);
                //if (endAngle < -180)
                //{
                //    endAngle = 360 + (endAngle);
                //}
                startAngle.eulerAngles = currentAngle.eulerAngles - new Vector3(0, 0, swingRange);

                if (currentAngle.eulerAngles.z > endAngle.eulerAngles.z)
                    isSwinging = false; //exit condition
                //Debug.Log("atkAngle.eulerAngles.z: " + (atkAngle.eulerAngles.z));
                //Debug.Log("endAngle.eulerAngles.z: " + (endAngle.eulerAngles.z));
                //Debug.Log("startAngle.eulerAngles.z: " + (startAngle.eulerAngles.z));

                if (WPN.transform.localEulerAngles.z >= atkAngle.z + swingRange)
                    isSwinging = false; //exit condition
                break;
        }

        //lerp angle stuff
        currentAngle.eulerAngles += new Vector3(0, 0, Time.deltaTime * CD * atkDir * 20);
    }

    public void Awake()
    {
        //main stats
        MaxHP = 60;
        CurrentHP = MaxHP;
        ATK = 5;
        SPD = 5;
        DEF = 10;
        f_MaxHP = MaxHP;
        f_ATK = ATK;
        f_SPD = SPD;
        f_DEF = DEF;

        //special stats
        CD = 10;
        JUMP = 1;
        CurrentJUMP = JUMP;
        SIZE = 2;
        WGHT = 5;

        f_CD = CD;
        f_JUMP = JUMP + 1;
        f_SIZE = SIZE;
        f_WGHT = WGHT;

        //set the size of the character
        transform.localScale = transform.localScale * f_SIZE;

        ITEM = Weapons.Sword;
        grounded = false;

        isSwinging = false;
        swingRange = 45;
        atkDir = 1;

        
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Left or Right
            switch (Input.GetAxisRaw("Horizontal"))
            {
                case -1:
                    MoveLeft();
                    break;
                case 1:
                    MoveRight();
                    break;
            }

            //Jump
            if ((Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Jump") == 1) && hasNotJumped)
            {
                Jump();
                //Debug.Log("wahoo");
            }
                
            //basically i wanna force the player to let go of the jump button
            if (Input.GetAxisRaw("Vertical") != 1 && Input.GetAxisRaw("Jump") != 1)
            {
                hasNotJumped = true;
            }



            //TODO: THIS QUARTENION STUFF! YOU CANT JUST USE EULERANGLES
            //https://docs.unity3d.com/ScriptReference/Quaternion-eulerAngles.html

            //currentAngle = WPN.transform.rotation.eulerAngles.z;
            //if (currentAngle > 180)
            //    currentAngle = currentAngle - 360;

            







            //Attacking
            if (Input.GetAxisRaw("Attack") == 1 && !isSwinging)
            {
                isSwinging = true;
                Debug.Log("swing");
                //get pointing angle
                //atkAngle = WPN.transform.rotation.eulerAngles.z;
                //Debug.Log("atkAngle before: " + atkAngle);
                //if (atkAngle > 180)
                //    atkAngle = atkAngle - 360;

                //Debug.Log("atkAngle after: " + atkAngle);

                currentEulerAngles = WPN.transform.rotation.eulerAngles;
                //if (currentEulerAngles.z > 180)
                    //currentEulerAngles.z = currentEulerAngles.z - 360;

                //moving the value of the Vector3 into Quanternion.eulerAngle format
                currentAngle.eulerAngles = currentEulerAngles;

                //moving the value of the Vector3 into Quanternion.eulerAngle format
                atkAngle.eulerAngles = currentEulerAngles;








                if (mousePos.x > transform.position.x)
                {
                    atkDir = -1;
                    Debug.Log("offset");
                    currentAngle.eulerAngles += new Vector3(0, 0, swingRange);
                    WPN.transform.rotation = currentAngle;
                }
                else
                {
                    atkDir = 1;

                    currentAngle.eulerAngles -= new Vector3(0, 0, swingRange);
                    WPN.transform.rotation = currentAngle;
                }
                    
                //start of the swing


                
            }

            //if we are swinging, stop pointing and swing
            if (isSwinging)
            {
                SwordAttack();
            }
            else
                PointWeapon(WPN, mousePos);




            //call gravity at the end of processing
            Gravity();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Recover();
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = false;
    }


}
