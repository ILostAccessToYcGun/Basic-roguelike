using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject door;
    private float closeElevation;
    private float openElevation;
    private bool isOpening;
    public SpriteRenderer doorRenderer;

    public enum DoorType { Entry, Exit };
    public DoorType doorType;

    public void OpenDoor()
    {
        isOpening = true;
    }

    public void CloseDoor()
    {
        isOpening = false;
    }

    private void Awake()
    {
        stageManager = FindAnyObjectByType<StageManager>();

        closeElevation =  transform.position.y; 
        openElevation =  transform.position.y + transform.localScale.y; 

        switch (doorType)
        {
            case DoorType.Entry:
                doorRenderer.color = new Color(50f / 255f, 1f, 150f / 255f, 1f);
                OpenDoor();
                break;
            case DoorType.Exit:
                doorRenderer.color = new Color(1f, 150f / 255f, 50f / 255f, 1f);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    if (collision.gameObject.transform.position.x < transform.position.x) //if the player enters from the left
        //        OpenDoor();
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (doorType == DoorType.Entry)
            {
                if (collision.gameObject.transform.position.x > transform.position.x) //if the player exits from the right
                {
                    CloseDoor();
                    
                    //close all doors in teh stage, 
                    //delete the previous stage
                    //move the stage poi
                }
            }
            
        }
            
            
    }

    private void Update()
    {
        if (isOpening)
        {
            door.transform.position = new Vector2(transform.position.x, Mathf.Lerp(door.transform.position.y, openElevation, Time.deltaTime * 5));
        }
        else
        {
            door.transform.position = new Vector2(transform.position.x, Mathf.Lerp(door.transform.position.y, closeElevation, Time.deltaTime * 5));
        }
    }
}
