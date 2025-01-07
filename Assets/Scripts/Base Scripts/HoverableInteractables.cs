using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverableInteractables : MonoBehaviour
{
    
    protected CameraMovement cam;
    public Image popUpUI;

    public List<Image> userInterfaces;
    public List<TextMeshProUGUI> texts;

    public float fadeDistance;
    public float unFadeWidth;
    protected float distanceToPlayer;

    public void CalculateFade()
    {
        foreach (Image ui in userInterfaces)
        {
            ui.color = new Color(ui.color.r, ui.color.g, ui.color.b, 1f - Mathf.Clamp(distanceToPlayer - unFadeWidth, 0f, fadeDistance) / fadeDistance);
        }
        foreach (TextMeshProUGUI txts in texts)
        {
            txts.color = new Color(txts.color.r, txts.color.g, txts.color.b, 1f - Mathf.Clamp(distanceToPlayer - unFadeWidth, 0f, fadeDistance) / fadeDistance);
        }
    }

    private void Awake()
    {
        cam = FindAnyObjectByType<CameraMovement>();
        userInterfaces.Add(popUpUI);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            popUpUI.gameObject.SetActive(true);
            cam.AddPOI(popUpUI.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            distanceToPlayer = Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x);
            CalculateFade();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            popUpUI.gameObject.SetActive(false);
            cam.RemovePOI(popUpUI.gameObject);
        }
    }
}
