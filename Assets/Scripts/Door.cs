using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject door;
    private float closeElevation = 0f;
    private float openElevation = 1f;
    public bool isOpening;
    public SpriteRenderer doorRenderer;

    public bool isLookingNextStage;
    public bool isLookingPreviousStage;

    public enum DoorType { Entry, Exit, Pass };
    public DoorType doorType;

    public void OpenDoor()
    {
        isOpening = true;
    }

    public void CloseDoor()
    {
        isOpening = false;
    }

    public void SetDoorStats()
    {
        if (door != null)
        {
            //closeElevation = 0;
            //openElevation = 5f;

            //closeElevation = transform.position.y;    
            //openElevation = transform.position.y + transform.localScale.y;

            switch (doorType)
            {
                case DoorType.Entry:
                    doorRenderer.color = new Color(50f / 255f, 1f, 150f / 255f, 1f);
                    OpenDoor();
                    break;
                case DoorType.Exit:
                    doorRenderer.color = new Color(1f, 150f / 255f, 50f / 255f, 1f);
                    CloseDoor();
                    break;
            }
        }
    }

    private void Awake()
    {
        stageManager = FindAnyObjectByType<StageManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (doorType == DoorType.Entry || doorType == DoorType.Pass)
            {
                if (collision.gameObject.transform.position.x < transform.position.x) //if the player enters from the left
                {
                    stageManager.NextStagePOI();
                    isLookingNextStage = true;
                    isLookingPreviousStage = false;
                }
                else if (collision.gameObject.transform.position.x > transform.position.x) //if the player enters from the right
                {
                    stageManager.PreviousStagePOI();
                    isLookingPreviousStage = true;
                    isLookingNextStage = false;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (doorType == DoorType.Entry)
            {
                if (collision.gameObject.transform.position.x > transform.position.x) //if the player exits from the right
                {
                    if (stageManager.currentClrCon == StageManager.ClearCondition.Intermission)
                    {
                        if (isOpening)
                        {
                            stageManager.StageBeginSequence();
                        }
                    }
                    //close all doors in teh stage, 
                    //delete the previous stage
                    //move the stage poi
                }
            }
            
            if (doorType == DoorType.Entry || doorType == DoorType.Pass)
            {
                if (collision.gameObject.transform.position.x > transform.position.x) //if the player exits from the right
                {
                    if (!isLookingNextStage)
                        stageManager.NextStagePOI();
                }
                else if (collision.gameObject.transform.position.x < transform.position.x)
                {
                    if (!isLookingPreviousStage)
                        stageManager.PreviousStagePOI();
                }
            }
            
        }
            
            
    }

    private void Update()
    {   
        if (door != null)
        {
            if (isOpening)
                door.transform.localPosition = new Vector2(0, Mathf.Lerp(door.transform.localPosition.y, openElevation, Time.deltaTime));
            else
                door.transform.localPosition = new Vector2(0, Mathf.Lerp(door.transform.localPosition.y, closeElevation, Time.deltaTime * 5));
            //it may look like the door snaps shut, but it is lerping, its just lag
        }
    }
}
