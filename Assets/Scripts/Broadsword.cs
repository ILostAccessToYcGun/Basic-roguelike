using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Broadsword : Units
{
    //okay, the unique things about the broadsword class is what weapon it holds, and how it attacks.
    //im pretty sure im wrong in saying that, cuz enemies might be able to get swords later but for the sake of tempo we keep coding

    //updating the data values because we are the sword class
    
    
    //for the sword attack we need to interrupt the weapon pointing animation, so we need something for that
    public void SwordAttack()
    {
        
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
        f_JUMP = JUMP;
        f_SIZE = SIZE;
        f_WGHT = WGHT;

        //set the size of the guy
        transform.localScale = transform.localScale * f_SIZE;

        ITEM = Weapons.Sword;
        grounded = false;
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            //Left or Right
            switch(Input.GetAxisRaw("Horizontal"))
            {
                case -1:
                    MoveLeft();
                    break;
                case 1:
                    MoveRight();
                    break;
            }

            //Jump
            if (Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Jump") == 1)
                Jump();

            PointWeapon(WPN, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //call gravity at the end of processing
            Gravity();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
