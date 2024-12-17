using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    public NormalEnemy enemyScript;
    private Broadsword playerScript;

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
