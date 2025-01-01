using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Characters player;

    private void Awake()
    {
        player = FindAnyObjectByType<Characters>();
    }
    void Update()
    {
        //TODO: Game state if statement to make sure we only do the fancy caamera movement if we're in battle or something
        //but for now we're always in battle :)
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //mouseScreenPosition - player.transform.position;

        //Debug.Log(mouseScreenPosition);
        //Debug.Log(player.transform.position);
        
        //TODO: okay this is working like I want tit to, but the player is a little shaky, ive run into this problem before and idk how to fix it.
        //maybe a lerp????
        this.transform.position = new Vector3((player.transform.position.x + (mouseScreenPosition.x - player.transform.position.x) / 2.5f), (player.transform.position.y + (mouseScreenPosition.y - player.transform.position.y) / 2.5f), this.transform.position.z);
    }
}
