using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static NormalEnemy;

public class Enemies : Units
{
    protected Characters player;
    protected StageManager stageManager;

    protected RaycastHit2D hit;
    protected LayerMask visionLayerMasks;
    public float visionRange;

    public enum AI { Roaming, Aggro }
    protected AI currentAI;

    /// <summary>
    /// Line of Sight Check
    /// </summary>
    public void LOStoPlayer()
    {
        if (!player.IsDestroyed())
        {
            // Cast a ray from the enemy to the player
            hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRange, visionLayerMasks);
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);

            if (hit)// If it hits the player or a wall, if there is a wall or ground inbetween the player and the enemy it will stay Roaming
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")) //only respond if it hits the player
                    currentAI = AI.Aggro;
                else
                    currentAI = AI.Roaming;
            }
            else
                currentAI = AI.Roaming;
        }
        else
            currentAI = AI.Roaming;
    }
        

    public override void DeathCheck()
    {
        //death /destory object later
        if (CurrentHP <= 0)
        {
            stageManager.UpdateEnemyCount(-1);
            Destroy(gameObject);

        }
    }
}


