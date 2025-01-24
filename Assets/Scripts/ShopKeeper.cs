using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ShopKeeper : HoverableInteractables
{
    public GameObject dialogueUI;
    public GameObject interactUI;
    public GameObject confirmStatsUI;
    public TextMeshProUGUI dialogueText;
    public BuffManager buffManager;
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

    public enum ShopPurchase { None, Buff, Crystal};
    public ShopPurchase purchase;

    public TextMeshProUGUI pointsAllocatedText;

    public StatCrystal statCrystal;
    public RarityCrystal rarityCrystal;

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
        dialogueText.text = dialogue;
    }

    public void InteractWithShopKeeper()
    {
        isTalking = true;
        if (GetTotalStatDifference() > 0 && purchase == ShopPurchase.None)
        {
            pointsAllocatedText.text = GetTotalStatDifference() + "/" + allocationLimit + " Points Allocated";
            dialogueTimer = 10f;
            interactUI.SetActive(false);
            cam.RemovePOI(interactUI);
            confirmStatsUI.SetActive(true);
            cam.AddPOI(confirmStatsUI);
        }
        else
        {
            switch (purchase)
            {
                case ShopPurchase.None:
                    ChangeDialogueText("See what you need.");
                    dialogueTimer = 3f;
                    interactUI.SetActive(false);
                    cam.RemovePOI(interactUI);
                    dialogueUI.SetActive(true);
                    cam.AddPOI(dialogueUI);
                    break;
                case ShopPurchase.Buff:
                    ChangeDialogueText("Hope you like the Buff!");
                    dialogueTimer = 3f;
                    interactUI.SetActive(false);
                    cam.RemovePOI(interactUI);
                    dialogueUI.SetActive(true);
                    cam.AddPOI(dialogueUI);
                    break;
                case ShopPurchase.Crystal:
                    ChangeDialogueText("Interesting Choice...");
                    dialogueTimer = 3f;
                    interactUI.SetActive(false);
                    cam.RemovePOI(interactUI);
                    dialogueUI.SetActive(true);
                    cam.AddPOI(dialogueUI);
                    break;
            }
        }
    }

    public void SetShopPurchase(ShopPurchase newPurchase)
    {
        if (purchase == ShopPurchase.None)
        {
            purchase = newPurchase;
            if (purchase != ShopPurchase.Crystal)
            {
                statCrystal.RevertToActual();
                rarityCrystal.RevertToActual();
                Debug.Log("aaaaaa");
            }
        }
    }

    private void Awake()
    {
        cam = FindAnyObjectByType<CameraMovement>();
        buffManager = FindAnyObjectByType<BuffManager>();
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

        buffManager.SetCurrentShop(this);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!isTalking && interactUI.activeSelf == false)
            {
                interactUI.SetActive(true);
                cam.AddPOI(interactUI);
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
                dialogueUI.SetActive(false);
                confirmStatsUI.SetActive(false);
                cam.RemovePOI(dialogueUI);
                cam.RemovePOI(confirmStatsUI);
                isTalking = false;
            }
        }

    }

    #region ShopKeeper UI Methods

    public void LockInStats()
    {
        confirmStatsUI.SetActive(false);
        statCrystal.ConvertPreviewToActual();
        rarityCrystal.ConvertPreviewToActual();

        allocationLimit = 0;
        SetShopPurchase(ShopPurchase.Crystal);
        buffManager.ClearAllAliveBuffs();
        
    }

    public void NuhUh()
    {
        confirmStatsUI.SetActive(false);
        ChangeDialogueText("Not to your taste?..");
        dialogueTimer = 3f;
        interactUI.SetActive(false);
        cam.RemovePOI(interactUI);
        dialogueUI.SetActive(true);
        cam.AddPOI(dialogueUI);
    }

    #endregion

}
