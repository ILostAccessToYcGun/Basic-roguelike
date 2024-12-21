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
    public int CurrentHP;
    protected int ATK;
    protected int SPD;    
    protected int DEF; //I havent figured out the damage calculation yet

    //----------Special Stats----------//
    protected int CD; //(ms)?
    protected int CurrentJUMP;
    protected int JUMP;
    protected float SIZE; //size multiplier
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
    public float f_SIZE;
    public int f_WGHT;

    //other stats
    public enum Weapons { Sword, Gun, Bomb, None}
    public Weapons ITEM;
    public GameObject WPN; //ref to the weapon the guy is holding
    public Quaternion pointAngle;

    protected float gravity;
    protected bool grounded = false;
    protected bool hasNotJumped;

    public bool isAttacked;

    protected Rigidbody2D rb;

    

    protected void InitializeStats()
    {
        //main stats
        CurrentHP = MaxHP;
        f_MaxHP = MaxHP;
        f_ATK = ATK;
        f_SPD = SPD;
        f_DEF = DEF;

        //special stats
        CurrentJUMP = JUMP;
        f_CD = CD;
        f_JUMP = JUMP;
        f_SIZE = SIZE;
        f_WGHT = WGHT;

        ITEM = Weapons.None;

        //set the size of the character
        transform.localScale = transform.localScale * f_SIZE;

        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;


    }

    protected void MoveRight()
    { gameObject.transform.position += new Vector3(1, 0, 0) * f_SPD * Time.deltaTime; }

    protected void MoveLeft()
    { gameObject.transform.position += new Vector3(-1, 0, 0) * f_SPD * Time.deltaTime; }

    protected void Jump()
    {
        if (CurrentJUMP != 0)
        {
            CurrentJUMP--;
            grounded = false;
            hasNotJumped = false;
            gravity = -7.5f * f_SIZE; //jump height, can be changed? maybe should be a variable
        }
    }

    protected void Gravity() //all units are affected by gravity
    {
        transform.position += new Vector3(0, -gravity , 0) * Time.deltaTime;
        if (!grounded)
            gravity += 10 * f_WGHT * Time.deltaTime;
        //instead of running an else statement here, I will make it on the collision where the gravity gets reset i think that will cut down on performace 
    }

    protected void Recover()
    {
        gravity = 0;
        grounded = true;
        hasNotJumped = true;
        CurrentJUMP = f_JUMP;
    }

    //TODO: Optimize these calculations theres gonna be a lot of these
    protected void PointWeapon(GameObject _weapon, Vector2 target)
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

            float dif = Mathf.DeltaAngle(_weapon.transform.localEulerAngles.z, currAngle);


            pointAngle.eulerAngles += new Vector3(0, 0, dif - 90);
            //point the weapon towards the mouse
            _weapon.transform.rotation = pointAngle;

            //evetually i want a thing so that the sword point weapon isnt dead on, cuz thats called a spear
        }
    }

    public void TakeDamage(int damageSource)
    {
        CurrentHP -= damageSource;
        isAttacked = true;
    }

    public virtual void DeathCheck() //TODO: currently with the virtual overrides (big pog btw) i basically cant use any of the og death check, i wanna fix that somehow
    {
        //death /destory object later
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Recover();
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.transform.position.y < transform.position.y - (0.5f * f_SIZE))
            Recover();
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.transform.position.y < transform.position.y - (0.5f * f_SIZE))
            Recover();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            grounded = false;
    }
}
