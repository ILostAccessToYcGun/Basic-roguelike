using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public NormalEnemy enemyScript;
    private Broadsword playerScript;

    private void Awake()
    {
        playerScript = GetComponentInParent<Broadsword>();
    }


    //Edit the script to stop occasionaly double damage
    //probably add the tagged enemies into a list and change the list after the attack is complete
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isSwingingSword)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyScript = collision.gameObject.GetComponent<NormalEnemy>();
                if (enemyScript.isAttacked == false)
                    enemyScript.TakeDamage(playerScript.f_ATK);
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
}
