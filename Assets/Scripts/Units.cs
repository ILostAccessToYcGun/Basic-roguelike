using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    //f_ indicates The final value, after buffs are applied
    //i think i can change these publics to protected but not yet

    //______________________________BASE STATS______________________________//
    //----------Main Stats----------//
    protected int MaxHP;
    protected int CurrentHP;
    protected int ATK;
    protected int SPD;    
    protected int DEF; //I havent figured out the damage calculation yet

    //----------Special Stats----------//
    protected int CD; //(ms)?
    protected int CurrentJUMP;
    protected int JUMP;
    protected int SIZE; //size multiplier
    protected int WGHT;

    //______________________________FINAL STATS______________________________//
    //----------Main Stats----------//
    public int f_MaxHP;
    public int f_ATK;
    public int f_SPD;
    public int f_DEF;

    //----------Special Stats----------//
    public int f_CD;
    public int f_JUMP;
    public int f_SIZE;
    public int f_WGHT;

    //other stats
    public enum Weapons { Sword, Gun, Bomb, None}
    public Weapons ITEM;
    public GameObject WPN; //ref to the weapon the guy is holding

    public float gravity;
    protected bool grounded;
    protected bool hasNotJumped;

    public Quaternion pointAngle;

    public void MoveRight()
    { gameObject.transform.position += new Vector3(1, 0, 0) * f_SPD * Time.deltaTime; }

    public void MoveLeft()
    { gameObject.transform.position += new Vector3(-1, 0, 0) * f_SPD * Time.deltaTime; }


    public void Jump()
    {
        if (CurrentJUMP != 0)
        {
            CurrentJUMP--;
            grounded = false;
            hasNotJumped = false;
            gravity = -15; //jump height, can be changed? maybe should be a variable
        }
    }

    public void Gravity() //all units are affected by gravity
    {
        transform.position += new Vector3(0, -gravity , 0) * Time.deltaTime;
        if (!grounded)
            gravity += 10 * f_WGHT * Time.deltaTime;
        //instead of running an else statement here, I will make it on the collision where the gravity gets reset i think that will cut down on performace 
    }

    public void Recover()
    {
        gravity = 0;
        grounded = true;
        hasNotJumped = true;
        CurrentJUMP = f_JUMP;
    }

    //TODO: Optimize these calculations theres gonna be a lot of these
    public void PointWeapon(GameObject _weapon, Vector2 target)
    {
        //if we're actually holding a weapon
        if (_weapon != null)
        {
            //gets the onscreen location of the mouse
            // convert mouse position into world coordinates
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get direction you want to point at
            Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

            float currAngle = Mathf.Asin(direction.y) * Mathf.Rad2Deg;

            if (mouseScreenPosition.x < transform.position.x)
            {
                currAngle = 180 - currAngle;
            }

            //TODO: you're gonna have to do some math here... for it to work correctly

            float dif = Mathf.DeltaAngle(_weapon.transform.localEulerAngles.z, currAngle);


            pointAngle.eulerAngles += new Vector3(0, 0, dif - 90);
            //point the weapon towards the mouse
            _weapon.transform.rotation = pointAngle;

            //evetually i want a thing so that the sword point weapon isnt dead on, cuz thats called a spear
        }
    }
}
