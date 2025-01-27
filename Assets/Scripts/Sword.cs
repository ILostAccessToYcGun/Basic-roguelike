using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public NormalEnemy enemyScript;
    private Broadsword playerScript;
    //private RunStatisticsManager rsManager;

    public List<Enemies> tagList;
    public bool isTagInList;

    private void Awake()
    {
        playerScript = GetComponentInParent<Broadsword>();
        //rsManager = FindAnyObjectByType<RunStatisticsManager>();
    }

    public void ResetTags()
    {
        foreach (Enemies tagged in tagList)
        {
            tagged.isAttacked = false;
        }

        tagList.Clear();
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

                isTagInList = false;
                foreach (Enemies tagged in tagList)
                {
                    if (tagged == enemyScript)
                        isTagInList = true;
                }

                if (enemyScript.isAttacked == false && isTagInList == false)
                {
                    enemyScript.TakeDamage(playerScript.f_ATK);
                    tagList.Add(enemyScript);
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
}
