using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ShopKeeper : HoverableInteractables
{
    public GameObject DialogueUI;
    public GameObject InteractUI;
    public TextMeshProUGUI DialogueText;
    public bool isTalking;
    private float dialogueTimer;

    #region All changeable stats

    public int saCommon;
    public int saUncommon;
    public int saRare;
    public int saEpic;
    public int saLegendary;
    public int saMaxHP;
    public int saATK;
    public int saSPD;
    public int saDEF;
    public int saATKSPD;
    public int saJUMP;
    public int saREG;

    #endregion

    public int allocationLimit;


    public int GetTotalStatDifference()
    {
        int total = 0;

        total += Mathf.Abs(saCommon);
        total += Mathf.Abs(saUncommon);
        total += Mathf.Abs(saRare);
        total += Mathf.Abs(saEpic);
        total += Mathf.Abs(saLegendary);
        total += Mathf.Abs(saMaxHP);
        total += Mathf.Abs(saATK);
        total += Mathf.Abs(saSPD);
        total += Mathf.Abs(saDEF);
        total += Mathf.Abs(saATKSPD);
        total += Mathf.Abs(saJUMP);
        total += Mathf.Abs(saREG);

        return total;
    }
    public void SetAllocationLimit(int limit)
    {
        allocationLimit = limit;
    }

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
        SetAllocationLimit(3);

        saCommon = 0;
        saUncommon = 0;
        saRare = 0;
        saEpic = 0;
        saLegendary = 0;
        saMaxHP = 0;
        saATK = 0;
        saSPD = 0;
        saDEF = 0;
        saATKSPD = 0;
        saJUMP = 0;
        saREG = 0;
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
            if (isTalking)
            {
                DialogueUI.SetActive(false);
                cam.RemovePOI(DialogueUI);
                isTalking = false;
            }
        }
    }
}
