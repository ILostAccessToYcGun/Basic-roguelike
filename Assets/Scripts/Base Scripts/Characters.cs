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
            Jump();

        //basically i wanna force the player to let go of the jump button
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
