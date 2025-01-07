using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopKeeper : HoverableInteractables
{
    public GameObject DialogueUI;
    public GameObject InteractUI;
    public TextMeshProUGUI DialogueText;
    public bool isTalking;
    private float dialogueTimer;

    public void ChangeDialogueText(string dialogue)
    {
        DialogueText.text = dialogue;
    }

    public void InteractWithShopKeeper()
    {
        isTalking = true;
        dialogueTimer = 3f;
        InteractUI.SetActive(false);
        cam.RemovePOI(InteractUI);
        DialogueUI.SetActive(true);
        cam.AddPOI(DialogueUI);
    }

    private void Awake()
    {
        cam = FindAnyObjectByType<CameraMovement>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!isTalking && InteractUI.activeSelf == false)
            {
                InteractUI.SetActive(true);
                cam.AddPOI(InteractUI);
            }
            distanceToPlayer = Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x);
            CalculateFade();
        }
            
    }

    private void Update()
    {
        if (dialogueTimer > 0)
        {
            dialogueTimer -= Time.deltaTime;
        }
        else
        {
            DialogueUI.SetActive(false);
            cam.RemovePOI(DialogueUI);
            isTalking = false;
        }
    }
}
