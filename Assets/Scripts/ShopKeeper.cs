using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject DialogueUI;
    public GameObject InteractUI;
    public TextMeshProUGUI DialogueText;
    private bool isTalking;
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
        DialogueUI.SetActive(true);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            if (!isTalking && InteractUI.activeSelf == false)
            {
                InteractUI.SetActive(true);
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            InteractUI.SetActive(false);
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
            isTalking = false;
        }
    }
}
