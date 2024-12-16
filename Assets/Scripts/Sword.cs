using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    public NormalEnemy enemyScript;
    private Broadsword playerScript;
    //public int hitCounter; //TODO: I should decide if this is a modifyable value.
    //as the system currently stands, it means the sword will only attack the first enemey hit, even if more enemies are caught in the swing

    private void Awake()
    {
        playerScript = GetComponentInParent<Broadsword>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isSwingingSword)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyScript = collision.gameObject.GetComponent<NormalEnemy>();
                if (enemyScript.isAttacked == false)
                {
                    enemyScript.TakeDamage(playerScript.f_ATK);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (enemyScript != collision.gameObject.GetComponent<NormalEnemy>()) //POG optimisation
                enemyScript = collision.gameObject.GetComponent<NormalEnemy>();
            enemyScript.isAttacked = false;

        }
    }


    //this method should be in unit i think,
    //If i hit an enemy with a sword swing,
    //I wanna not be able to hit that enemy for every tick that the collider is in it.
    //So I need a boolean in all enenmies that is turned on when it is hit, and turns off after the attacking collider leaves the trigger

}
