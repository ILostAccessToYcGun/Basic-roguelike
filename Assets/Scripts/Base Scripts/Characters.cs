using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : Units
{
    public UIManager uiManager;
    public Buff currentBuff; //eventually I should make this general to include all interactables
    public ShopKeeper shopKeeper;
    public GameManager gameManager;


    protected Vector3 mousePos;
    protected float attackTimer;
    protected int attackDirection = 1;
    protected Quaternion currentAngle;

    protected bool notUsedBuffYet = true;

    protected override void InitializeStats()
    {
        base.InitializeStats();
        uiManager = FindAnyObjectByType<UIManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.SetPlayer(this);
    }

    protected void Movement()
    {
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
            if (gameManager.currentGameState == GameManager.GameState.Gameplay)
            {
                gameManager.ChangeGameState(GameManager.GameState.Paused);
            }
            else if (gameManager.currentGameState == GameManager.GameState.Paused)
            {
                gameManager.ChangeGameState(GameManager.GameState.Gameplay);
            }


            //if (uiManager.PauseMenuUI.activeSelf == true)
            //    uiManager.PauseMenuToggle(false);
            //else if (uiManager.PlayerStatsUI.activeSelf == true)
            //    uiManager.PlayerStatsUIToggle(false);
            //else
            //    uiManager.PauseMenuToggle(true);
            ////TODO: game manager change state here
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentBuff != null && notUsedBuffYet)
            {
                notUsedBuffYet = false;
                currentBuff.GivePlayerBuff();
            }
            if (shopKeeper != null && !shopKeeper.isTalking)
            {
                shopKeeper.InteractWithShopKeeper();
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            notUsedBuffYet = true;
        }

    }

    public override void DeathCheck()
    {
        if (CurrentHP <= 0)
        {
            //change the game state in the game manager
            gameManager.ChangeGameState(GameManager.GameState.Dead);
            uiManager.UpdateRunStatistics();
            Debug.Log("ur actually so bad at this game");
            //Destroy(gameObject);
        }
    }

    


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buff"))
        {
            currentBuff = collision.gameObject.GetComponent<Buff>();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ShopKeeper"))
        {
            shopKeeper = collision.gameObject.GetComponent<ShopKeeper>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buff"))
        {
            currentBuff = null;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ShopKeeper"))
        {
            shopKeeper = null;
        }
    }
}
