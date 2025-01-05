using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    protected int currentJUMP;
    protected int JUMP;
    protected int jHeight; //jump height
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
    public int f_jHeight; //honestly not really happy with this stat
    public int f_WGHT;

    //other stats
    public enum Weapons { Sword, Gun, Bomb, None}
    public Weapons ITEM;
    public GameObject WPN; //ref to the weapon the guy is holding
    public Quaternion pointAngle;

    //TODO: change the access modifiers
    public float gravity;
    public bool grounded = false;
    public bool hasNotJumped;
    public bool floored = false;

    public bool walled = false;
    public bool isWallJumping;
    public float wallJumpDuration;
    public enum wallJumpDirection { Left, Right }
    public wallJumpDirection wallJumpDir = wallJumpDirection.Left;

    public bool isAttacked;
    protected Rigidbody2D rb;

    //UI
    public Slider hpBar;

    

    protected void InitializeStats()
    {
        //main stats
        CurrentHP = MaxHP;
        f_MaxHP = MaxHP;
        f_ATK = ATK;
        f_SPD = SPD;
        f_DEF = DEF;

        //special stats
        currentJUMP = JUMP;
        f_CD = CD;
        f_JUMP = JUMP;
        f_jHeight = jHeight;
        f_WGHT = WGHT;

        ITEM = Weapons.None;


        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        hpBar = GetComponentInChildren<Slider>();
        UpdateHPBar();
    }

    protected void MoveRight()
    { gameObject.transform.position += new Vector3(1, 0, 0) * f_SPD * Time.deltaTime; }

    protected void MoveLeft()
    { gameObject.transform.position += new Vector3(-1, 0, 0) * f_SPD * Time.deltaTime; }

    protected void Jump()
    {
        if (currentJUMP != 0)
        {
            if (walled && !isWallJumping && !floored)
            {
                isWallJumping = true;
                f_SPD += SPD/2;
                wallJumpDuration = 0.5f;
            }
            currentJUMP--;
            grounded = false;
            floored = false;
            hasNotJumped = false;
            gravity = -7.5f * f_jHeight; //jump height, can be changed? maybe should be a variable



        }
    }

    /// <summary>
    /// This function goes inside the else block of a if (!isWallJumping) {} else {--here--} statement
    /// </summary>
    protected void WallJumpMovement()
    {
        switch (wallJumpDir)
        {
            case wallJumpDirection.Left:
                MoveLeft();
                break;
            case wallJumpDirection.Right:
                MoveRight();
                break;
        }

        if (wallJumpDuration <= 0)
        {
            isWallJumping = false;
            f_SPD -= SPD / 2;
        }

        else
            wallJumpDuration -= Time.deltaTime;
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
        floored = true;
        walled = false;
        hasNotJumped = true;
        currentJUMP = f_JUMP;
    }

    protected void WallRecover(float wallX)
    {
        walled = true;

        if ( wallX < transform.position.x)
            wallJumpDir = wallJumpDirection.Right;
        else
            wallJumpDir = wallJumpDirection.Left;

        if (gravity >= 0)
        { 
            hasNotJumped = true;
            currentJUMP = f_JUMP;
        }
        if (gravity > 5)
        {
            gravity = 5;
            grounded = true;
            
        }
    }


    /*OKay I need to write down some more details on the wall jump
     * The wall jump will perform a similar height jump as the regular jump, but with speed in the opposite direction as the wall. (needs a refence to the wall we are sliding against)
     * if we are in a bottom corner (grounded and walled) the system should prioritize jumping regularly first.
     * being more specific, if the character is sliding to the right of the wall, the character will jump diagonal right,
     * if the character is sliding to the left of the wall, the character will diagona jump left
     * when the wall jump happens, the player will not be able to take action directionally for the duration of the jump

    */

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
                currAngle = 180 - currAngle;

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
        UpdateHPBar();
        isAttacked = true;
    }

    public void Heal(int healSource)
    {
        if (healSource > f_MaxHP - CurrentHP)
            CurrentHP += f_MaxHP - CurrentHP;
        else
            CurrentHP += healSource;
        UpdateHPBar();
    }

    public virtual void DeathCheck() //TODO: currently with the virtual overrides (big pog btw) i basically cant use any of the og death check, i wanna fix that somehow
    {
        //death /destory object later
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHPBar()
    {
        hpBar.value = (float)CurrentHP / (float)f_MaxHP;
        //Debug.Log((float)CurrentHP / (float)f_MaxHP);
    }

    private void OnCollisionStay2D(Collision2D collision) //TODO: NEEDS ATTENTION HERE
    {
        //Ground Recovery
        if (gravity >= 5)
        {
            if (collision.transform.position.y < transform.position.y - (0.5f * transform.localScale.y))
            {
                //if (collision.transform.position.x + (0.5f * collision.transform.localScale.x) > transform.position.x - (0.5f * f_SIZE) && collision.transform.position.x - (0.5f * collision.transform.localScale.x) < transform.position.x + (0.5f * f_SIZE))
                if (!(collision.transform.position.x + (0.5f * collision.transform.localScale.x) < transform.position.x - (0.4f * transform.localScale.y) || collision.transform.position.x - (0.5f * collision.transform.localScale.x) > transform.position.x + (0.4f * transform.localScale.y))) //TODO: make this not a ! statement
                {
                    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                        Recover();
                    else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        Recover();
                    else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                        Recover();

                    //Debug.Log("AHHHHHHHHHHHHHHH Ground Recover");
                }
            }
        }
        else //OKAY THIS IS STARTING TO FEEL BETTER, NOW MAKE WALL JUMP, AN ALTERNATIVE TO JUMP
        {
            if (collision.transform.position.y + (0.5f * collision.transform.localScale.y) < transform.position.y - (0.5f * transform.localScale.y))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    Recover();
                else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    Recover();
                else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                    Recover();

                //Debug.Log("Basic Ground Recover");
            }
        }

        //Wall Recovery
        if (collision.transform.position.y - (0.5f * collision.transform.localScale.y) < transform.position.y + (0.5f * transform.localScale.y) &&
        collision.transform.position.y + (0.5f * collision.transform.localScale.y) > transform.position.y - (0.5f * transform.localScale.y))
        {
            if (collision.transform.position.x < transform.position.x - (0.5f * transform.localScale.y) ||
            collision.transform.position.x > transform.position.x + (0.5f * transform.localScale.y))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (!isWallJumping)
                    {
                        WallRecover(collision.transform.position.x);
                        //Debug.Log("Wall Recover");
                    }
                }
            }
        }






    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            grounded = false;
            walled = false;
            floored = false;
        }
            

    }
}
