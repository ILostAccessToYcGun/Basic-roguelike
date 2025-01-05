using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : Units
{
    protected UIManager uiManager;
    public Buff currentBuff;

    protected Vector3 mousePos;
    protected float attackTimer;
    protected int attackDirection = 1;
    protected Quaternion currentAngle;

    protected bool isEscapeReleased;
    protected bool notUsedBuffYet;

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

    protected void OtherControls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEscapeReleased = false;
            
            if (uiManager.PauseMenuUI.activeSelf == true)
                uiManager.PauseMenuToggle(false);
            else if (uiManager.PlayerStatsUI.activeSelf == true)
                uiManager.PlayerStatsUIToggle(false);
            else
                uiManager.PauseMenuToggle(true);
            //TODO: game manager change state here
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F) && notUsedBuffYet)
        {
            if (currentBuff != null)
            {
                notUsedBuffYet = false;
                currentBuff.GivePlayerBuff();
            }
        }
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.F))
        {
            notUsedBuffYet = true;
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buff"))
        {
            currentBuff = collision.gameObject.GetComponent<Buff>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buff"))
        {
            currentBuff = null;
        }
    }
}
