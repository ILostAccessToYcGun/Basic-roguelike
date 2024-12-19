using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static NormalEnemy;

public class Enemies : Units
{
    public Characters player;

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

    
}
