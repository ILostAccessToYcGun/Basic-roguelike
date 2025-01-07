using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Characters player;
    public List<GameObject> POIs;
    public float cameraMidPoint;
    public float cameraYoffset;

    public float t;


    public void AddPOI(GameObject poi)
    {
        POIs.Add(poi);
    }
    public void RemovePOI(GameObject poi)
    {
        POIs.Remove(poi);
    }

    private void Awake()
    {
        player = FindAnyObjectByType<Characters>();
    }
    void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        
        cameraMidPoint = POIs.Count + 2f;

        float posX = (player.transform.position.x +                 (mouseScreenPosition.x - player.transform.position.x) / cameraMidPoint);
        float posY = (player.transform.position.y + cameraYoffset + (mouseScreenPosition.y - player.transform.position.y) / cameraMidPoint);

        foreach (GameObject poi in POIs)
        {
            posX += (poi.transform.position.x - player.transform.position.x) / cameraMidPoint;
            posY += (poi.transform.position.y - player.transform.position.y) / cameraMidPoint;
        }

        //TODO: okay this is working like I want it to, but the player is a little shaky, ive run into this problem before and idk how to fix it.
        //maybe a lerp????

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, posX, t), Mathf.Lerp(transform.position.y, posY, t), transform.position.z);

        //transform.position = 
    }
}
