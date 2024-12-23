using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : Units
{
    protected Vector3 mousePos;
    protected float attackTimer;
    protected int attackDirection = 1;
    protected Quaternion currentAngle;

    protected void Movement()
    {
        DeathCheck();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!isWallJumping)
        {
            switch (Input.GetAxisRaw("Horizontal"))// Left or Right
            {
                case -1:
                    MoveLeft();
                    break;
                case 1:
                    MoveRight();
                    break;
            }
        }
        else
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

        //Jumping
        if ((Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Jump") == 1) && hasNotJumped)
            Jump();
        if (Input.GetAxisRaw("Vertical") != 1 && Input.GetAxisRaw("Jump") != 1)
            hasNotJumped = true;

        //Gravity
        Gravity();
        
    }

    public override void DeathCheck()
    {
        if (CurrentHP <= 0)
        {
            //change the game state in the game manager
            Debug.Log("ur actually so bad at this game");
            Destroy(gameObject);

        }
    }
}
